using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;
namespace Clinic.Application.Features.Doctors.UpdateDoctorAchievement;

/// <summary>
///     GetProfileUser Handler
/// </summary>
public class UpdateDoctorAchievementByIdHandler : IFeatureHandler<UpdateDoctorAchievementByIdRequest, UpdateDoctorAchievementByIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public UpdateDoctorAchievementByIdHandler(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
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
    public async Task<UpdateDoctorAchievementByIdResponse> ExecuteAsync(
        UpdateDoctorAchievementByIdRequest request,
        CancellationToken cancellationToken
    )
    {


        // Get userId from sub type jwt
        var userId = Guid.Parse(
            _contextAccessor.HttpContext.User.FindFirstValue(claimType: JwtRegisteredClaimNames.Sub)
        );

        // Found user by userId
        var foundUser =
            await _unitOfWork.UpdateDoctorAchievementRepository.GetDoctorByIdAsync(
                    userId,
                    cancellationToken
                );

        // Responds if userId is not found
        if (Equals(objA: foundUser, objB: default))
        {
            return new UpdateDoctorAchievementByIdResponse()
            {
                StatusCode = UpdateDoctorAchievementByIdResponseStatusCode.USER_IS_NOT_FOUND
            };
        }

        var isSucced = await UpdateUserProfileAsync(foundUser, request, cancellationToken);

        if (!isSucced)
        {
            return new UpdateDoctorAchievementByIdResponse()
            {
                StatusCode = UpdateDoctorAchievementByIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            };
        }

        // Response successfully.
        return new UpdateDoctorAchievementByIdResponse()
        {
            StatusCode = UpdateDoctorAchievementByIdResponseStatusCode.OPERATION_SUCCESS,
        };

    }


    public async Task<bool> UpdateUserProfileAsync(User user, UpdateDoctorAchievementByIdRequest request, CancellationToken cancellationToken)
    {

        // If the user has a related doctor, update the doctor entity
        if (user.Doctor != null)
        {
            user.Doctor.Achievement = request.Achievement ?? user.Doctor.Achievement;
        }


        // Save the updated user back to the repository
        return await _unitOfWork.UpdateDoctorAchievementRepository.UpdateDoctorAchievementByIdCommandAsync(user, cancellationToken);
    }
}

