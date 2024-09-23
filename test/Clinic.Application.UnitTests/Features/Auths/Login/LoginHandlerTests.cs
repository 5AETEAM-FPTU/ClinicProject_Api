using AutoFixture;
using AutoFixture.AutoMoq;
using Clinic.Application.Commons.Token.AccessToken;
using Clinic.Application.Commons.Token.RefreshToken;
using Clinic.Application.Features.Auths.Login;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.UnitOfWorks;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace Clinic.Application.UnitTests.Features.Auths.Login;

public class LoginHandlerTests
{
    private readonly IFixture _fixture;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<UserManager<User>> _userManagerMock;
    private readonly Mock<SignInManager<User>> _signInManagerMock;
    private readonly Mock<IRefreshTokenHandler> _refreshTokenHandlerMock;
    private readonly Mock<IAccessTokenHandler> _accessTokenHandlerMock;

    public LoginHandlerTests()
    {
        // Initialize AutoFixture with AutoMoq to automatically create mocks
        _fixture = new Fixture().Customize(new AutoMoqCustomization());

        // Remove ThrowingRecursionBehavior
        _fixture
            .Behaviors.OfType<ThrowingRecursionBehavior>()
            .ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        // Set up individual mocks
        _unitOfWorkMock = _fixture.Freeze<Mock<IUnitOfWork>>();
        _userManagerMock = _fixture.Freeze<Mock<UserManager<User>>>();
        _signInManagerMock = _fixture.Freeze<Mock<SignInManager<User>>>();
        _refreshTokenHandlerMock = _fixture.Freeze<Mock<IRefreshTokenHandler>>();
        _accessTokenHandlerMock = _fixture.Freeze<Mock<IAccessTokenHandler>>();
    }

    [Fact]
    public async Task ExecuteAsync_Should_Return_Success_When_Login_Is_Correct()
    {
        // Arrange
        var loginRequest = _fixture.Create<LoginRequest>(); // Create a test request using AutoFixture
        var user = _fixture.Create<User>(); // Generate a test user object

        // Mock user found by username
        _userManagerMock.Setup(um => um.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(user); // Mock finding the user by username

        // Mock password check to be successful
        _userManagerMock
            .Setup(um => um.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>()))
            .ReturnsAsync(true); // Mock the password check success

        // Mock getting roles
        _userManagerMock
            .Setup(um => um.GetRolesAsync(It.IsAny<User>()))
            .ReturnsAsync(new[] { "UserRole" }); // Mock user role fetching

        // Mock generating refresh token and access token
        _refreshTokenHandlerMock
            .Setup(rt => rt.Generate(It.IsAny<int>()))
            .Returns("TestRefreshToken");
        _accessTokenHandlerMock
            .Setup(at =>
                at.GenerateSigningToken(
                    It.IsAny<System.Collections.Generic.List<System.Security.Claims.Claim>>()
                )
            )
            .Returns("TestAccessToken");

        // Mock repository calls for user and refresh token creation
        _unitOfWorkMock
            .Setup(uow =>
                uow.LoginRepository.CreateRefreshTokenCommandAsync(
                    It.IsAny<RefreshToken>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(true); // Mock successful token creation

        _unitOfWorkMock
            .Setup(uow =>
                uow.LoginRepository.GetUserByUserIdQueryAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(new User()); // Mock successful user query

        var handler = new LoginHandler(
            _unitOfWorkMock.Object,
            _userManagerMock.Object,
            _signInManagerMock.Object,
            _refreshTokenHandlerMock.Object,
            _accessTokenHandlerMock.Object
        );

        // Act
        var result = await handler.ExecuteAsync(loginRequest, CancellationToken.None);

        // Assert
        result.StatusCode.Should().Be(LoginResponseStatusCode.OPERATION_SUCCESS); // Ensure login success
        result.ResponseBody.Should().NotBeNull();
        result.ResponseBody.AccessToken.Should().Be("TestAccessToken");
        result.ResponseBody.RefreshToken.Should().Be("TestRefreshToken");
    }

    [Fact]
    public async Task ExecuteAsync_Should_Return_UserNotFound_When_User_Does_Not_Exist()
    {
        // Arrange
        var loginRequest = _fixture.Create<LoginRequest>(); // Create a test request using AutoFixture

        // Mock user not found
        _userManagerMock
            .Setup(um => um.FindByNameAsync(It.IsAny<string>()))
            .ReturnsAsync((User)null); // Return null to simulate user not found

        var handler = new LoginHandler(
            _unitOfWorkMock.Object,
            _userManagerMock.Object,
            _signInManagerMock.Object,
            _refreshTokenHandlerMock.Object,
            _accessTokenHandlerMock.Object
        );

        // Act
        var result = await handler.ExecuteAsync(loginRequest, CancellationToken.None);

        // Assert
        result.StatusCode.Should().Be(LoginResponseStatusCode.USER_IS_NOT_FOUND); // Ensure user not found response
    }

    [Fact]
    public async Task ExecuteAsync_Should_Return_IncorrectPassword_When_Password_Is_Wrong()
    {
        // Arrange
        var loginRequest = _fixture.Create<LoginRequest>(); // Create a test request using AutoFixture
        var user = _fixture.Create<User>(); // Generate a test user object

        // Mock user found by username
        _userManagerMock.Setup(um => um.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(user); // Mock finding the user by username

        // Mock password check failure
        _userManagerMock
            .Setup(um => um.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>()))
            .ReturnsAsync(false); // Simulate password mismatch

        _signInManagerMock
            .Setup(sm => sm.CheckPasswordSignInAsync(It.IsAny<User>(), It.IsAny<string>(), true))
            .ReturnsAsync(SignInResult.Failed); // Simulate sign-in failure

        var handler = new LoginHandler(
            _unitOfWorkMock.Object,
            _userManagerMock.Object,
            _signInManagerMock.Object,
            _refreshTokenHandlerMock.Object,
            _accessTokenHandlerMock.Object
        );

        // Act
        var result = await handler.ExecuteAsync(loginRequest, CancellationToken.None);

        // Assert
        result.StatusCode.Should().Be(LoginResponseStatusCode.USER_PASSWORD_IS_NOT_CORRECT); // Ensure password incorrect response
    }
}
