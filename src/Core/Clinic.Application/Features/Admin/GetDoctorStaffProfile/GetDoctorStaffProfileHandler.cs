using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Features.Doctors.GetProfileDoctor;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Clinic.Application.Features.Admin.GetDoctorStaffProfile;

public sealed class GetDoctorStaffProfileHandler
    : IFeatureHandler<GetDoctorStaffProfileRequest, GetDoctorStaffProfileResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public GetDoctorStaffProfileHandler(
        IUnitOfWork unitOfWork,
        IHttpContextAccessor contextAccessor
    )
    {
        _unitOfWork = unitOfWork;
        _contextAccessor = contextAccessor;
    }

    public async Task<GetDoctorStaffProfileResponse> ExecuteAsync(
        GetDoctorStaffProfileRequest command,
        CancellationToken ct
    )
    {
        // Get userId from sub type jwt
        var userId = Guid.Parse(
            _contextAccessor.HttpContext.User.FindFirstValue(claimType: JwtRegisteredClaimNames.Sub)
        );

        // Check role doctor from role type jwt
        var role = _contextAccessor.HttpContext.User.FindFirstValue(claimType: "role");
        if (!role.Equals("admin") && !role.Equals("staff"))
        {
            return new()
            {
                StatusCode = GetDoctorStaffProfileResponseStatusCode.ROLE_IS_NOT_PERMISSION,
            };
        }

        // Found user by userId
        var foundUser =
            await _unitOfWork.GetDoctorStaffProfileRepository.GetDoctorByDoctorIdQueryAsync(
                userId: command.DoctorId,
                cancellationToken: ct
            );

        // Responds if userId is not found
        if (Equals(objA: foundUser, objB: default))
        {
            return new() { StatusCode = GetDoctorStaffProfileResponseStatusCode.USER_IS_NOT_FOUND };
        }
        return new()
        {
            StatusCode = GetDoctorStaffProfileResponseStatusCode.OPERATION_SUCCESS,
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
                        item => new GetDoctorStaffProfileResponse.Body.UserDetail.ResponseSpecialties()
                        {
                            Id = item.Specialty.Id,
                            SpecialtyName = item.Specialty.Name,
                        }
                    ),
                    IsOnDuty = foundUser.Doctor.IsOnDuty,
                },
            },
        };
    }
}
