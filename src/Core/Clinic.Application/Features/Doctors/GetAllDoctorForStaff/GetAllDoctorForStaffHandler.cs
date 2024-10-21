using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Commons.Pagination;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;

namespace Clinic.Application.Features.Doctors.GetAllDoctorForStaff;

/// <summary>
///     GetAllDoctorForStaff Handler
/// </summary>
public class GetAllDoctorForStaffHandler
    : IFeatureHandler<GetAllDoctorForStaffRequest, GetAllDoctorForStaffResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public GetAllDoctorForStaffHandler(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
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
    public async Task<GetAllDoctorForStaffResponse> ExecuteAsync(
        GetAllDoctorForStaffRequest request,
        CancellationToken cancellationToken
    )
    {
        // Get role user
        var role = _contextAccessor.HttpContext.User.FindFirstValue(claimType: "role");

        // Respond if role is not staff
        if (!Equals(objA: role, objB: "staff"))
        {
            return new() { StatusCode = GetAllDoctorForStaffResponseStatusCode.ROLE_IS_NOT_STAFF };
        }

        // Find all doctor for booking query.
        var doctors = await _unitOfWork.GetAllDoctorForStaffRepository.FindAllDoctorsQueryAsync(
            pageIndex: request.PageIndex,
            pageSize: request.PageSize,
            keyWord: request.KeyWord,
            cancellationToken: cancellationToken
        );

        // Count all the doctors.
        var countDoctor =
            await _unitOfWork.GetAllDoctorForStaffRepository.CountAllDoctorsQueryAsync(
                keyWord: request.KeyWord,
                cancellationToken: cancellationToken
            );

        // Response successfully.
        return new GetAllDoctorForStaffResponse()
        {
            StatusCode = GetAllDoctorForStaffResponseStatusCode.OPERATION_SUCCESS,

            ResponseBody = new()
            {
                userDetails = new PaginationResponse<GetAllDoctorForStaffResponse.Body.UserDetail>()
                {
                    Contents = doctors.Select(
                        doctor => new GetAllDoctorForStaffResponse.Body.UserDetail()
                        {
                            Id = doctor.UserId,
                            AvatarUrl = doctor.User.Avatar,
                            FullName = doctor.User.FullName,
                            Gender = new()
                            {
                                Id = doctor.User.Gender.Id,
                                GenderName = doctor.User.Gender.Name,
                                GenderConstant = doctor.User.Gender.Constant,
                            },
                            Specialties = doctor.DoctorSpecialties.Select(
                                item => new GetAllDoctorForStaffResponse.Body.UserDetail.ResponseSpecialties()
                                {
                                    Id = item.Specialty.Id,
                                    SpecialtyName = item.Specialty.Name,
                                    SpecialtyConstant = item.Specialty.Constant,
                                }
                            ),
                            IsOnDuty = doctor.IsOnDuty,
                        }
                    ),
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize,
                    TotalPages = (int)Math.Ceiling((double)countDoctor / request.PageSize),
                },
            },
        };
    }
}
