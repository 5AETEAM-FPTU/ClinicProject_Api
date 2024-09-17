﻿using Clinic.Application.Commons.Abstractions.GetProfileUser;
using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Clinic.Application.Features.Users.GetProfileDoctor;

/// <summary>
///     GetProfileDoctor Handler
/// </summary>
public class GetProfileDoctorHandler : IFeatureHandler<GetProfileDoctorRequest, GetProfileDoctorResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public GetProfileDoctorHandler(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
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
    public async Task<GetProfileDoctorResponse> ExecuteAsync(
        GetProfileDoctorRequest request,
        CancellationToken cancellationToken
    )
    {
        // Get userId from sub type jwt
        var userId = Guid.Parse(
            _contextAccessor.HttpContext.User.FindFirstValue(claimType: JwtRegisteredClaimNames.Sub)
        );

        // Found user by userId
        var foundUser =
            await _unitOfWork.GetProfileDoctorRepository.GetProfileDoctorByDoctorIdQueryAsync(
                userId: userId,
                cancellationToken: cancellationToken
            );

        // Responds if userId is not found
        if (Equals(objA: foundUser, objB: default))
        {
            return new GetProfileDoctorResponse()
            {
                StatusCode = GetProfileDoctorResponseStatusCode.USER_IS_NOT_FOUND
            };
        }

        // Is user not temporarily removed.
        //var isUserNotTemporarilyRemoved =
        //    await _unitOfWork.AuthFeature.LoginRepository.IsUserTemporarilyRemovedQueryAsync(
        //        userId: userId,
        //        cancellationToken: cancellationToken
        //    );

        // Responds if current user is temporarily removed.
        //if (!isUserNotTemporarilyRemoved)
        //{
        //    return new()
        //    {
        //        StatusCode = GetProfileUserResponseStatusCode.USER_IS_TEMPORARILY_REMOVED
        //    };
        //}

        // Response successfully.
        return new GetProfileDoctorResponse()
        {
            StatusCode = GetProfileDoctorResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new()
            {
                User = new()
                {
                    // common attribute user
                    Username = foundUser.UserName,
                    PhoneNumber = foundUser.PhoneNumber,
                    AvatarUrl = foundUser.Avatar,
                    FullName = foundUser.FullName,

                    Gender = foundUser.Doctor.Gender
                    DOB = foundUser.Doctor.DOB,
                    Address = foundUser.Doctor.Address,
                    Description = foundUser.Doctor.Description,
                    Achievement = foundUser.Doctor.Achievement,
                    Specialty = foundUser.Doctor.Specialty,
                    Position = foundUser.Doctor.Position
                }
            }
        };
    }
}