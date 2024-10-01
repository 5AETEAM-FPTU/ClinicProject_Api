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

namespace Clinic.Application.Features.Doctors.GetProfileDoctor;

/// <summary>
///     GetProfileDoctor Handler
/// </summary>
public class GetProfileDoctorHandler
    : IFeatureHandler<GetProfileDoctorRequest, GetProfileDoctorResponse>
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

        // Check role doctor from role type jwt
        var role = _contextAccessor.HttpContext.User.FindFirstValue(claimType: "role");
        if (!role.Equals("doctor") && !role.Equals("staff"))
        {
            return new()
            {
                StatusCode = GetProfileDoctorResponseStatusCode.ROLE_IS_NOT_DOCTOR_OR_STAFF,
            };
        }

        // Found user by userId
        var foundUser = await _unitOfWork.GetProfileDoctorRepository.GetDoctorByDoctorIdQueryAsync(
            userId: userId,
            cancellationToken: cancellationToken
        );

        // Responds if userId is not found
        if (Equals(objA: foundUser, objB: default))
        {
            return new GetProfileDoctorResponse()
            {
                StatusCode = GetProfileDoctorResponseStatusCode.USER_IS_NOT_FOUND,
            };
        }

        // Is user not temporarily removed.
        var isUserTemporarilyRemoved =
            await _unitOfWork.GetProfileDoctorRepository.IsUserTemporarilyRemovedQueryAsync(
                userId: userId,
                cancellationToken: cancellationToken
            );

        // Responds if current user is temporarily removed.
        if (isUserTemporarilyRemoved)
        {
            return new()
            {
                StatusCode = GetProfileDoctorResponseStatusCode.USER_IS_TEMPORARILY_REMOVED,
            };
        }

        // Response successfully.
        return new GetProfileDoctorResponse()
        {
            StatusCode = GetProfileDoctorResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new()
            {
                User = new()
                {
                    Username = foundUser.UserName,
                    PhoneNumber = foundUser.PhoneNumber,
                    AvatarUrl = foundUser.Avatar,
                    FullName = foundUser.FullName,
                    DOB = foundUser.Doctor.DOB,
                    Address = foundUser.Doctor.Address,
                    Description = foundUser.Doctor.Description,
                    Achievement = foundUser.Doctor.Achievement,
                    Gender = new() { Id = foundUser.Gender.Id, GenderName = foundUser.Gender.Name },
                    Position = new()
                    {
                        Id = foundUser.Doctor.Position.Id,
                        PositionName = foundUser.Doctor.Position.Name,
                    },
                    Specialties = foundUser.Doctor.DoctorSpecialties.Select(
                        item => new GetProfileDoctorResponse.Body.UserDetail.ResponseSpecialties()
                        {
                            Id = item.Specialty.Id,
                            SpecialtyName = item.Specialty.Name,
                        }
                    ),
                    //AppointmentStatus = new()

                },
            },
        };
    }
}
