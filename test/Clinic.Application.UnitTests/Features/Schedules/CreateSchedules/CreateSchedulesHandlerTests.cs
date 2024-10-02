using System.Security.Claims;
using AutoFixture;
using AutoFixture.AutoMoq;
using Clinic.Application.Commons.FIleObjectStorage;
using Clinic.Application.Features.Schedules.CreateSchedules;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;
using Moq;
using static Clinic.Application.Features.Schedules.CreateSchedules.CreateSchedulesRequest;

namespace Clinic.Application.UnitTests.Features.Schedules.CreateSchedules;

public class CreateSchedulesHandlerTests
{
    private readonly IFixture _fixture;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<UserManager<User>> _userManagerMock;
    private readonly Mock<IDefaultUserAvatarAsUrlHandler> _defaultUserAvatarHandlerMock;
    private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;

    public CreateSchedulesHandlerTests()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());

        _unitOfWorkMock = _fixture.Freeze<Mock<IUnitOfWork>>();
        _userManagerMock = _fixture.Freeze<Mock<UserManager<User>>>();
        _defaultUserAvatarHandlerMock = _fixture.Freeze<Mock<IDefaultUserAvatarAsUrlHandler>>();
        _httpContextAccessorMock = _fixture.Freeze<Mock<IHttpContextAccessor>>();

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, Guid.NewGuid().ToString()),
            new Claim("role", "doctor")
        };
        var identity = new ClaimsIdentity(claims, "Test");
        var userPrincipal = new ClaimsPrincipal(identity);
        var httpContext = new DefaultHttpContext { User = userPrincipal };
        _httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContext);
    }

    [Fact]
    public async Task ExecuteAsync_Should_Return_Success_When_Login_Is_Correct()
    {
        // Arrange
        var handler = new CreateSchedulesHandler(
            _unitOfWorkMock.Object,
            _userManagerMock.Object,
            _defaultUserAvatarHandlerMock.Object,
            _httpContextAccessorMock.Object
        );

        var request = new CreateSchedulesRequest
        {
            TimeSlots = new List<TimeSlot>
            {
                new TimeSlot
                {
                    StartTime = DateTime.UtcNow.AddHours(1),
                    EndTime = DateTime.UtcNow.AddHours(2)
                }
            }
        };

        _unitOfWorkMock
            .Setup(repo =>
                repo.CreateSchedulesRepository.AreOverLappedSlotTimes(
                    It.IsAny<IEnumerable<Schedule>>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(false);

        _unitOfWorkMock
            .Setup(repo =>
                repo.CreateSchedulesRepository.CreateSchedulesAsync(
                    It.IsAny<IEnumerable<Schedule>>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(true);

        // Act
        var result = await handler.ExecuteAsync(request, CancellationToken.None);

        // Assert
        Assert.Equal(CreateSchedulesResponseStatusCode.OPERATION_SUCCESS, result.StatusCode);
    }

    [Fact]
    public async Task ExecuteAsync_Should_Return_Forbidden_When_User_Is_Not_Doctor_Or_Staff()
    {
        // Arrange:
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, Guid.NewGuid().ToString()),
            new Claim("role", "patient")
        };
        var identity = new ClaimsIdentity(claims, "Test");
        var userPrincipal = new ClaimsPrincipal(identity);
        var httpContext = new DefaultHttpContext { User = userPrincipal };
        _httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContext);

        var handler = new CreateSchedulesHandler(
            _unitOfWorkMock.Object,
            _userManagerMock.Object,
            _defaultUserAvatarHandlerMock.Object,
            _httpContextAccessorMock.Object
        );

        var request = new CreateSchedulesRequest
        {
            TimeSlots = new List<TimeSlot>
            {
                new TimeSlot
                {
                    StartTime = DateTime.UtcNow.AddHours(1),
                    EndTime = DateTime.UtcNow.AddHours(2)
                }
            }
        };

        // Act
        var result = await handler.ExecuteAsync(request, CancellationToken.None);

        // Assert
        Assert.Equal(CreateSchedulesResponseStatusCode.FORBIDEN, result.StatusCode);
    }

    [Fact]
    public async Task ExecuteAsync_Should_Return_TimeslotIsExist_When_Overlapping_Schedules_Exist()
    {
        // Arrange
        var handler = new CreateSchedulesHandler(
            _unitOfWorkMock.Object,
            _userManagerMock.Object,
            _defaultUserAvatarHandlerMock.Object,
            _httpContextAccessorMock.Object
        );

        var request = new CreateSchedulesRequest
        {
            TimeSlots = new List<TimeSlot>
            {
                new TimeSlot
                {
                    StartTime = DateTime.UtcNow.AddHours(1),
                    EndTime = DateTime.UtcNow.AddHours(2)
                }
            }
        };

        _unitOfWorkMock
            .Setup(repo =>
                repo.CreateSchedulesRepository.AreOverLappedSlotTimes(
                    It.IsAny<IEnumerable<Schedule>>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(true);

        // Act
        var result = await handler.ExecuteAsync(request, CancellationToken.None);

        // Assert
        Assert.Equal(CreateSchedulesResponseStatusCode.TIMESLOT_IS_EXIST, result.StatusCode);
    }

    [Fact]
    public async Task ExecuteAsync_Should_Return_DatabaseOperationFail_When_Db_Operation_Fails()
    {
        // Arrange
        var handler = new CreateSchedulesHandler(
            _unitOfWorkMock.Object,
            _userManagerMock.Object,
            _defaultUserAvatarHandlerMock.Object,
            _httpContextAccessorMock.Object
        );

        var request = new CreateSchedulesRequest
        {
            TimeSlots = new List<TimeSlot>
            {
                new TimeSlot
                {
                    StartTime = DateTime.UtcNow.AddHours(1),
                    EndTime = DateTime.UtcNow.AddHours(2)
                }
            }
        };

        _unitOfWorkMock
            .Setup(repo =>
                repo.CreateSchedulesRepository.AreOverLappedSlotTimes(
                    It.IsAny<IEnumerable<Schedule>>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(false);

        _unitOfWorkMock
            .Setup(repo =>
                repo.CreateSchedulesRepository.CreateSchedulesAsync(
                    It.IsAny<IEnumerable<Schedule>>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(false);

        // Act
        var result = await handler.ExecuteAsync(request, CancellationToken.None);

        // Assert
        Assert.Equal(CreateSchedulesResponseStatusCode.DATABASE_OPERATION_FAIL, result.StatusCode);
    }
}
