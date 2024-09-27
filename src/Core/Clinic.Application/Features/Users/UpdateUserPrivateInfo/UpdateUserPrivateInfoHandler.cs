using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Features.Doctors.UpdatePrivateDoctorInfo;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;
namespace Clinic.Application.Features.Users.UpdateUserPrivateInfo;

/// <summary>
///     GetProfileUser Handler
/// </summary>
public class UpdateUserPrivateInfoHandler : IFeatureHandler<UpdateUserPrivateInfoRequest, UpdateUserPrivateInfoResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public UpdateUserPrivateInfoHandler(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
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
    public async Task<UpdateUserPrivateInfoResponse> ExecuteAsync(
        UpdateUserPrivateInfoRequest request,
        CancellationToken cancellationToken
    )
    {


        // Get userId from sub type jwt
        var userId = Guid.Parse(
            _contextAccessor.HttpContext.User.FindFirstValue(claimType: JwtRegisteredClaimNames.Sub)
        );

        // Found user by userId
        var foundUser =
            await _unitOfWork.UpdateUserPrivateInfoRepository.GetUserByIdAsync(
                    userId,
                    cancellationToken
                );

        // Responds if userId is not found
        if (Equals(objA: foundUser, objB: default))
        {
            return new UpdateUserPrivateInfoResponse()
            {
                StatusCode = UpdateUserPrivateInfoResponseStatusCode.USER_IS_NOT_FOUND
            };
        }

        var isSucced = await UpdateUserProfileAsync(foundUser, request, cancellationToken);

        if (!isSucced)
        {
            return new UpdateUserPrivateInfoResponse()
            {
                StatusCode = UpdateUserPrivateInfoResponseStatusCode.DATABASE_OPERATION_FAIL,
            };
        }

        // Response successfully.
        return new UpdateUserPrivateInfoResponse()
        {
            StatusCode = UpdateUserPrivateInfoResponseStatusCode.OPERATION_SUCCESS,
        };

    }

    public async Task<bool> UpdateUserProfileAsync(User user, UpdateUserPrivateInfoRequest request, CancellationToken cancellationToken)
    {

        // Update the user entity with the values from the DTO
        user.FullName = request.FullName ?? user.FullName;
        user.PhoneNumber = request.PhoneNumber ?? user.PhoneNumber;

        // If the user has a related Patient, update the Patient entity
        if (user.Patient != null)
        {
            //user.Patient.Gender = request.Gender ?? user.Patient.Gender;
            user.Patient.DOB = request.DOB != default ? request.DOB : user.Patient.DOB;
            user.Patient.Address = request.Address ?? user.Patient.Address;
        }


        // Save the updated user back to the repository
        return await _unitOfWork.UpdateUserPrivateInfoRepository.UpdateUserPrivateInfoByIdCommandAsync(user, cancellationToken);
    }
}

