using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;

namespace Clinic.Application.Features.MedicalReports.GetMedicalReportsForStaff;

/// <summary>
///     GetMedicalReportsForStaff Handler
/// </summary>
public class GetMedicalReportsForStaffHandler
    : IFeatureHandler<GetMedicalReportsForStaffRequest, GetMedicalReportsForStaffResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public GetMedicalReportsForStaffHandler(
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
    public async Task<GetMedicalReportsForStaffResponse> ExecuteAsync(
        GetMedicalReportsForStaffRequest request,
        CancellationToken cancellationToken
    )
    {
        // Check role "Only doctor and staff can access"
        var role = _contextAccessor.HttpContext.User.FindFirstValue(claimType: "role");
        if (!role.Equals("staff") && !role.Equals("doctor"))
        {
            return new GetMedicalReportsForStaffResponse()
            {
                StatusCode = GetMedicalReportsForStaffResponseStatusCode.ROLE_IS_NOT_DOCTOR_STAFF,
            };
        }

        // Get all reports.
        var reports =
            await _unitOfWork.GetMedicalReportsForStaffRepository.FindAllMedicalReportByDoctorIdQueryAsync(
                request.Keyword,
                request.LastReportDate,
                cancellationToken: cancellationToken
            );

        // Response successfully.
        return new GetMedicalReportsForStaffResponse()
        {
            StatusCode = GetMedicalReportsForStaffResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new GetMedicalReportsForStaffResponse.Body()
            {
                GroupedReports = reports
                    .GroupBy(report => report.Appointment.Schedule.StartDate.Date)
                    .Select(group => new GetMedicalReportsForStaffResponse.Body.GroupedReport
                    {
                        DayOfDate = group.Key,
                        MedicalReports = group
                            .Select(
                                report => new GetMedicalReportsForStaffResponse.Body.GroupedReport.MedicalReport()
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
                                    ServiceOrderId = report.ServiceOrder.Id,
                                    MedicineOrderId = report.MedicineOrder.Id,
                                }
                            )
                            .ToList(),
                    })
                    .ToList(),
            },
        };
    }
}
