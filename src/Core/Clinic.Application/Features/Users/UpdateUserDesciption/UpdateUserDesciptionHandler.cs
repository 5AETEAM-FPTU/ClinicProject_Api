using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;
namespace Clinic.Application.Features.Users.UpdateUserDesciption;

/// <summary>
///     GetProfileUser Handler
/// </summary>
public class UpdateUserDesciptionHandler : IFeatureHandler<UpdateUserDesciptionRequest, UpdateUserDesciptionResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public UpdateUserDesciptionHandler(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
    {
        _unitOfWork = unitOfWork;
        _contextAccessor = contextAccessor;
    }

    /// <summary>
    ///     Entry of new request handler.
    /// </summary>
    /// <param name="request">
    ///     Request model.
    /// </param>
    /// <param name="ct">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     A task containing the response.
    public async Task<UpdateUserDesciptionResponse> ExecuteAsync(
        UpdateUserDesciptionRequest request,
        CancellationToken cancellationToken
    )
    {


        // Get userId from sub type jwt
        var userId = Guid.Parse(
            _contextAccessor.HttpContext.User.FindFirstValue(claimType: JwtRegisteredClaimNames.Sub)
        );

        // Found user by userId
        var foundUser =
            await _unitOfWork.UpdateUserDescriptionRepository.GetUserByIdAsync(
                    userId,
                    cancellationToken
                );

        // Responds if userId is not found
        if (Equals(objA: foundUser, objB: default))
        {
            return new UpdateUserDesciptionResponse()
            {
                StatusCode = UpdateUserDesciptionResponseStatusCode.USER_IS_NOT_FOUND
            };
        }

        var isSucced = await UpdateUserProfileAsync(foundUser, request, cancellationToken);

        if (!isSucced)
        {
            return new UpdateUserDesciptionResponse()
            {
                StatusCode = UpdateUserDesciptionResponseStatusCode.DATABASE_OPERATION_FAIL,
            };
        }

        // Response successfully.
        return new UpdateUserDesciptionResponse()
        {
            StatusCode = UpdateUserDesciptionResponseStatusCode.OPERATION_SUCCESS,
        };

    }

    public async Task<bool> UpdateUserProfileAsync(User user, UpdateUserDesciptionRequest request, CancellationToken cancellationToken)
    {

        // update description
        if (user.Patient != null)
        {
            user.Patient.Description = request.Description ?? user.Patient.Description;
        }
     
        // Save the updated user back to the repository
        return await _unitOfWork.UpdateUserDescriptionRepository.UpdateUserDescriptionByIdCommandAsync(user, cancellationToken);
    }
}

