using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Clinic.Application.Features.Doctors.GetAllMedicalReport;

/// <summary>
///     GetAllMedicalReport Handler
/// </summary>
public class GetAllMedicalReportHandler
    : IFeatureHandler<GetAllMedicalReportRequest, GetAllMedicalReportResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public GetAllMedicalReportHandler(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
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
    public async Task<GetAllMedicalReportResponse> ExecuteAsync(
        GetAllMedicalReportRequest request,
        CancellationToken cancellationToken
    )
    {
        // Get userId from sub type jwt
        var userId = Guid.Parse(
            _contextAccessor.HttpContext.User.FindFirstValue(claimType: JwtRegisteredClaimNames.Sub)
        );

        // Check role "Only doctor and staff can access"
        var role = _contextAccessor.HttpContext.User.FindFirstValue(claimType: "role");
        if (!role.Equals("staff") && !role.Equals("doctor"))
        {
            return new GetAllMedicalReportResponse()
            {
                StatusCode = GetAllMedicalReportResponseStatusCode.ROLE_IS_NOT_DOCTOR_STAFF,
            };
        }

        // Get all reports.
        var reports =
            await _unitOfWork.GetAllMedicalReportRepository.FindAllMedicalReportByDoctorIdQueryAsync(
                request.Keyword,
                request.LastReportDate,
                request.PageSize,
                userId,
                cancellationToken: cancellationToken
            );

        // Response successfully.
        return new GetAllMedicalReportResponse()
        {
            StatusCode = GetAllMedicalReportResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new GetAllMedicalReportResponse.Body()
            {
                GroupedReports = reports
                    .GroupBy(report => report.Appointment.Schedule.StartDate.Date)
                    .Select(group => new GetAllMedicalReportResponse.Body.GroupedReport
                    {
                        DayOfDate = group.Key,
                        MedicalReports = group
                            .Select(
                                report => new GetAllMedicalReportResponse.Body.GroupedReport.MedicalReport()
                                {
                                    PatientId = report.PatientInformation.Id,
                                    ReportId = report.Id,
                                    FullName = report.PatientInformation.FullName,
                                    Avatar = report.Appointment.Patient.User.Avatar,
                                    PhoneNumber = report.PatientInformation.PhoneNumber,
                                    Gender = report.PatientInformation.Gender,
                                    StartTime = report.Appointment.Schedule.StartDate,
                                    EndTime = report.Appointment.Schedule.EndDate,
                                    Age = DateTime.Now.Year - report.PatientInformation.DOB.Year,
                                    Diagnosis = report.Diagnosis,
                                    ServiceOrderId = report.ServiceOrder.Id
                                }
                            )
                            .ToList(),
                    })
                    .ToList(),
            },
        };
    }
}
