using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Commons.Abstractions.UpdateDoctorDescription;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;
namespace Clinic.Application.Features.Users.UpdateDoctorDescription;

/// <summary>
///     GetProfileUser Handler
/// </summary>
public class UpdateDoctorDescriptionByIdHandler : IFeatureHandler<UpdateDoctorDescriptionByIdRequest, UpdateDoctorDescriptionByIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public UpdateDoctorDescriptionByIdHandler(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
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
    public async Task<UpdateDoctorDescriptionByIdResponse> ExecuteAsync(
        UpdateDoctorDescriptionByIdRequest request,
        CancellationToken cancellationToken
    )
    {


        // Get userId from sub type jwt
        var userId = Guid.Parse(
            _contextAccessor.HttpContext.User.FindFirstValue(claimType: JwtRegisteredClaimNames.Sub)
        );

        // Found user by userId
        var foundUser =
            await _unitOfWork.UpdateDoctorDescriptionRepository.GetDoctorByIdAsync(
                    userId,
                    cancellationToken
                );

        // Responds if userId is not found
        if (Equals(objA: foundUser, objB: default))
        {
            return new UpdateDoctorDescriptionByIdResponse()
            {
                StatusCode = UpdateDoctorDescriptionByIdResponseStatusCode.USER_IS_NOT_FOUND
            };
        }

        var isSucced = await UpdateUserProfileAsync(foundUser, request, cancellationToken);

        if (!isSucced)
        {
            return new UpdateDoctorDescriptionByIdResponse()
            {
                StatusCode = UpdateDoctorDescriptionByIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            };
        }

        // Response successfully.
        return new UpdateDoctorDescriptionByIdResponse()
        {
            StatusCode = UpdateDoctorDescriptionByIdResponseStatusCode.OPERATION_SUCCESS,
        };

    }

    public async Task<bool> UpdateUserProfileAsync(User user, UpdateDoctorDescriptionByIdRequest request, CancellationToken cancellationToken)
    {

        // If the user has a related doctor, update the doctor entity
        if (user.Doctor != null)
        {
            user.Doctor.Description = request.Description ?? user.Doctor.Description;
        }


        // Save the updated user back to the repository
        return await _unitOfWork.UpdateDoctorDescriptionRepository.UpdateDoctorDescriptionByIdCommandAsync(user, cancellationToken);
    }
}

