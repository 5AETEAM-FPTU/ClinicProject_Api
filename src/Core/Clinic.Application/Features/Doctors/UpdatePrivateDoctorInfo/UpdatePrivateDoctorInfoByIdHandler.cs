using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Clinic.Application.Features.Doctors.UpdatePrivateDoctorInfo;

/// <summary>
///     GetProfileUser Handler
/// </summary>
public class UpdatePrivateDoctorInfoByIdHandler
    : IFeatureHandler<UpdatePrivateDoctorInfoByIdRequest, UpdatePrivateDoctorInfoByIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public UpdatePrivateDoctorInfoByIdHandler(
        IUnitOfWork unitOfWork,
        IHttpContextAccessor contextAccessor
    )
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
    public async Task<UpdatePrivateDoctorInfoByIdResponse> ExecuteAsync(
        UpdatePrivateDoctorInfoByIdRequest request,
        CancellationToken cancellationToken
    )
    {
        // Is genderId found
        var isGenderFound =
            await _unitOfWork.UpdatePrivateDoctorInfoRepository.IsGenderFoundByIdQueryAsync(
                genderId: request.GenderId,
                cancellationToken: cancellationToken
            );

        // Respond if genderId is not found
        if (!isGenderFound)
        {
            return new()
            {
                StatusCode = UpdatePrivateDoctorInfoByIdResponseStatusCode.GENDER_ID_IS_NOT_FOUND,
            };
        }

        // Is positionId found
        var positionIdFound =
            await _unitOfWork.UpdatePrivateDoctorInfoRepository.IsPositionFoundByIdQueryAsync(
                positionId: request.PositionId,
                cancellationToken: cancellationToken
            );

        // Respond if genderId is not found
        if (!positionIdFound)
        {
            return new()
            {
                StatusCode = UpdatePrivateDoctorInfoByIdResponseStatusCode.POSITION_ID_IS_NOT_FOUND,
            };
        }

        foreach (Guid specialtyId in request.SpecialtiesId)
        {
            var isSpecialtyFound =
                await _unitOfWork.UpdatePrivateDoctorInfoRepository.IsSpecialtyFoundByIdQueryAsync(
                    specialtyId: specialtyId,
                    cancellationToken: cancellationToken
                );
            if (!isSpecialtyFound)
            {
                return new()
                {
                    StatusCode =
                        UpdatePrivateDoctorInfoByIdResponseStatusCode.SPECIALTY_ID_IS_NOT_FOUND,
                };
            }
        }

        // Get userId from sub type jwt
        var userId = Guid.Parse(
            _contextAccessor.HttpContext.User.FindFirstValue(claimType: JwtRegisteredClaimNames.Sub)
        );

        // Found user by userId
        var foundUser = await _unitOfWork.UpdatePrivateDoctorInfoRepository.GetDoctorByIdAsync(
            userId,
            cancellationToken
        );

        // Responds if userId is not found
        if (Equals(objA: foundUser, objB: default))
        {
            return new UpdatePrivateDoctorInfoByIdResponse()
            {
                StatusCode = UpdatePrivateDoctorInfoByIdResponseStatusCode.USER_IS_NOT_FOUND,
            };
        }

        var isSucced = await UpdateUserProfileAsync(foundUser, request, cancellationToken);

        if (!isSucced)
        {
            return new UpdatePrivateDoctorInfoByIdResponse()
            {
                StatusCode = UpdatePrivateDoctorInfoByIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            };
        }

        // Response successfully.
        return new UpdatePrivateDoctorInfoByIdResponse()
        {
            StatusCode = UpdatePrivateDoctorInfoByIdResponseStatusCode.OPERATION_SUCCESS,
        };
    }

    public async Task<bool> UpdateUserProfileAsync(
        User user,
        UpdatePrivateDoctorInfoByIdRequest request,
        CancellationToken cancellationToken
    )
    {
        // Update the user entity with the values from the DTO
        user.FullName = request.FullName ?? user.FullName;
        user.PhoneNumber = request.PhoneNumber ?? user.PhoneNumber;
        user.GenderId = request.GenderId;

        // If the user has a related doctor, update the doctor entity
        if (user.Doctor != null)
        {
            user.Doctor.DOB = request.DOB != default ? request.DOB : user.Doctor.DOB;
            user.Doctor.Address = request.Address ?? user.Doctor.Address;
            user.Doctor.PositionId = request.PositionId;
        }

        // Save the updated user back to the repository
        return await _unitOfWork.UpdatePrivateDoctorInfoRepository.UpdatePrivateDoctorInfoByIdCommandAsync(
            user,
            request.SpecialtiesId,
            cancellationToken
        );
    }
}
