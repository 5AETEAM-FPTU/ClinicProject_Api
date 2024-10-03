using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Security.Claims;
using System.Linq;

namespace Clinic.Application.Features.Users.GetRecentMedicalReport;

/// <summary>
///     GetRecentMedicalReport Handler
/// </summary>
public class GetRecentMedicalReportHandler : IFeatureHandler<GetRecentMedicalReportRequest, GetRecentMedicalReportResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public GetRecentMedicalReportHandler(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
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
    public async Task<GetRecentMedicalReportResponse> ExecuteAsync(
        GetRecentMedicalReportRequest request,
        CancellationToken cancellationToken
    )
    {
        // Get userId from sub type jwt
        var userId = Guid.Parse(
            _contextAccessor.HttpContext.User.FindFirstValue(claimType: JwtRegisteredClaimNames.Sub)
        );

        // Check role "Only user can access"
        var role = _contextAccessor.HttpContext.User.FindFirstValue(claimType: "role");
        if (!role.Equals("user"))
        {
            return new GetRecentMedicalReportResponse()
            {
                StatusCode = GetRecentMedicalReportResponseStatusCode.ROLE_IS_NOT_USER
            };
        }

        // Get all reports.
        var reports = await _unitOfWork.GetRecentMedicalReportRepository.FindAllMedicalReportByUserIdQueryAsync(
            userId,
            cancellationToken: cancellationToken
        );


        // Response successfully.
        return new GetRecentMedicalReportResponse()
        {
            StatusCode = GetRecentMedicalReportResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new GetRecentMedicalReportResponse.Body()
            {
               MedicalReports = reports.Select(report => new GetRecentMedicalReportResponse.Body.MedicalReport()
               {
                   FullName = report.Appointment.Schedule.Doctor.User.FullName,
                   Avatar = report.Appointment.Schedule.Doctor.User.Avatar,
                   Date = report.Appointment.Schedule.StartDate.Date,
                   StartTime = report.Appointment.Schedule.StartDate,
                   EndTime = report.Appointment.Schedule.EndDate,
                   Diagnosis = report.Diagnosis,
                   DoctorId = report.Appointment.Schedule.DoctorId,
                   ReportId = report.Id,
                   Title = report.Name,
               })
            }
        };
    }
}
