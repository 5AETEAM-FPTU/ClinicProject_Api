using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Features.UnitOfWorks;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System;
using Clinic.Application.Commons.Pagination;

namespace Clinic.Application.Features.Doctors.GetAllDoctorForBooking;

/// <summary>
///     GetAllDoctorForBooking Handler
/// </summary>
public class GetAllDoctorForBookingHandler : IFeatureHandler<GetAllDoctorForBookingRequest, GetAllDoctorForBookingResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public GetAllDoctorForBookingHandler(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
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
    public async Task<GetAllDoctorForBookingResponse> ExecuteAsync(
        GetAllDoctorForBookingRequest request,
        CancellationToken cancellationToken
    )
    {
        // Check role user from role type jwt
        var role = _contextAccessor.HttpContext.User.FindFirstValue(claimType: "role");
        if (!role.Equals("user") )
        {
            return new()
            {
                StatusCode = GetAllDoctorForBookingResponseStatusCode.ROLE_IS_NOT_USER,
            };
        }

        // Find all doctor for booking query.
        var doctors = await _unitOfWork.GetAllDoctorForBookingRepository.FindAllDoctorForBookingQueryAsync(
            pageIndex: request.PageIndex,
            pageSize: request.PageSize,
            filterName: request.Name,
            specialtyId: request.SpecialtyId,
            genderId: request.GenderId,
            cancellationToken: cancellationToken
        );

        // Count all the doctors.
        var counDoctor = await _unitOfWork.GetAllDoctorForBookingRepository.CountAllDoctorsQueryAsync(
            cancellationToken: cancellationToken
        );

        // Response successfully.
        return new GetAllDoctorForBookingResponse()
        {
            StatusCode = GetAllDoctorForBookingResponseStatusCode.OPERATION_SUCCESS,

            ResponseBody = new()
            {
                userDetails = new PaginationResponse<GetAllDoctorForBookingResponse.Body.UserDetail>()
                {
                    Contents = doctors.Select(doctor => new GetAllDoctorForBookingResponse.Body.UserDetail()
                    {
                        Id = doctor.UserId,
                        Username = doctor.User.UserName,
                        PhoneNumber = doctor.User.PhoneNumber,
                        AvatarUrl = doctor.User.Avatar,
                        FullName = doctor.User.FullName,
                        DOB = doctor.DOB,
                        Address = doctor.Address,
                        Description = doctor.Description,
                        Achievement = doctor.Achievement,
                        Gender = new()
                        {
                            Id = doctor.User.Gender.Id,
                            GenderName = doctor.User.Gender.Name
                        },
                        Position = new()
                        {
                            Id = doctor.Position.Id,
                            PositionName = doctor.Position.Name,
                        },
                        Specialties = doctor.DoctorSpecialties.Select(
                        item => new GetAllDoctorForBookingResponse.Body.UserDetail.ResponseSpecialties()
                        {
                            Id = item.Specialty.Id,
                            SpecialtyName = item.Specialty.Name,
                        }
                         ),
                        Rating = doctor.Schedules
                        .Select(schedule => schedule?.Appointment)
                        .Select(appointment => appointment?.Feedback)
                        .Select(feedback => feedback.Vote)
                        .Average()
                    }),
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize,
                    TotalPages = (int)Math.Ceiling((double)counDoctor / request.PageSize)
                }
            }
        };
    }
}
