using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Commons.Pagination;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;

namespace Clinic.Application.Features.Doctors.GetRecentMedicalReportByUserId;

/// <summary>
///     GetRecentMedicalReportByUserId Handler
/// </summary>
public class GetRecentMedicalReportByUserIdHandler
    : IFeatureHandler<GetRecentMedicalReportByUserIdRequest, GetRecentMedicalReportByUserIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public GetRecentMedicalReportByUserIdHandler(
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
    public async Task<GetRecentMedicalReportByUserIdResponse> ExecuteAsync(
        GetRecentMedicalReportByUserIdRequest request,
        CancellationToken cancellationToken
    )
    {
        // Check role "Only user can access"
        var role = _contextAccessor.HttpContext.User.FindFirstValue(claimType: "role");
        if (!(role.Equals("doctor") || role.Equals("staff")))
        {
            return new GetRecentMedicalReportByUserIdResponse()
            {
                StatusCode =
                    GetRecentMedicalReportByUserIdResponseStatusCode.ROLE_IS_NOT_DOCTOR_STAFF,
            };
        }

        //Check if user not found
        var isUserFound =
            await _unitOfWork.GetRecentMedicalReportByUserIdRepository.IsUserFoundQueryAsync(
                request.UserId,
                cancellationToken
            );

        if (!isUserFound)
        {
            return new GetRecentMedicalReportByUserIdResponse()
            {
                StatusCode = GetRecentMedicalReportByUserIdResponseStatusCode.USER_ID_NOT_FOUND,
            };
        }

        //Count all reports
        var count =
            await _unitOfWork.GetRecentMedicalReportByUserIdRepository.CountAllMedicalReportByUserIdQueryAsync(
                cancellationToken
            );

        // Get all reports.
        var reports =
            await _unitOfWork.GetRecentMedicalReportByUserIdRepository.FindAllMedicalReportByUserIdQueryAsync(
                request.PageIndex,
                request.PageSize,
                request.UserId,
                cancellationToken: cancellationToken
            );

        // Response successfully.
        return new GetRecentMedicalReportByUserIdResponse()
        {
            StatusCode = GetRecentMedicalReportByUserIdResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new GetRecentMedicalReportByUserIdResponse.Body()
            {
                MedicalReports =
                    new PaginationResponse<GetRecentMedicalReportByUserIdResponse.Body.MedicalReport>()
                    {
                        Contents = reports.Select(
                            report => new GetRecentMedicalReportByUserIdResponse.Body.MedicalReport()
                            {
                                FullName = report.Appointment.Schedule.Doctor.User.FullName,
                                Avatar = report.Appointment.Schedule.Doctor.User.Avatar,
                                Date = report.Appointment.Schedule.StartDate.Date,
                                StartTime = report.Appointment.Schedule.StartDate,
                                EndTime = report.Appointment.Schedule.EndDate,
                                Diagnosis = report.Diagnosis,
                                DoctorId = report.Appointment.Schedule.Doctor.UserId,
                                ReportId = report.Id,
                                Title = report.Name,
                            }
                        ),
                        PageIndex = request.PageIndex,
                        PageSize = request.PageSize,
                        TotalPages = (int)Math.Ceiling((double)count / request.PageSize),
                    },
            },
        };
    }
}
