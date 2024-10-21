using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Clinic.Application.Features.Users.GetUserMedicalReport;

/// <summary>
///     GetUserMedicalReport Handler
/// </summary>
public class GetUserMedicalReportHandler
    : IFeatureHandler<GetUserMedicalReportRequest, GetUserMedicalReportResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public GetUserMedicalReportHandler(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
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
    public async Task<GetUserMedicalReportResponse> ExecuteAsync(
        GetUserMedicalReportRequest request,
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
            return new GetUserMedicalReportResponse()
            {
                StatusCode = GetUserMedicalReportResponseStatusCode.ROLE_IS_NOT_USER,
            };
        }

        // Found medical report by reportId
        var foundReport =
            await _unitOfWork.GetUserMedicalReportRepository.GetMedicalReportByIdQueryAsync(
                request.ReportId,
                cancellationToken
            );

        // Responds if reportId is not found
        if (Equals(objA: foundReport, objB: default))
        {
            return new GetUserMedicalReportResponse()
            {
                StatusCode = GetUserMedicalReportResponseStatusCode.MEDICAL_REPORT_NOT_FOUND,
            };
        }

        // Get isAppointmentHasFeedback
        var hasFeedback =
            await _unitOfWork.GetUserMedicalReportRepository.IsAppointmentHasFeedbackQueryAsync(
                request.ReportId,
                cancellationToken
            );

        // Response successfully.
        return new GetUserMedicalReportResponse()
        {
            StatusCode = GetUserMedicalReportResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new()
            {
                PatientInfor = new GetUserMedicalReportResponse.Body.Patient()
                {
                    PatientId = foundReport.PatientInformation.Id,
                    Address = foundReport.PatientInformation.Address,
                    PatientAvatar = foundReport.Appointment.Patient.User.Avatar,
                    DOB = foundReport.PatientInformation.DOB,
                    PatientName = foundReport.PatientInformation.FullName,
                    PatientGender = foundReport.PatientInformation.Gender,
                    PhoneNumber = foundReport.PatientInformation.PhoneNumber,
                },
                Detail = new GetUserMedicalReportResponse.Body.ReportDetail()
                {
                    BloodPressure = foundReport.BloodPresser,
                    Date = foundReport.Appointment.Schedule.StartDate,
                    Diagnosis = foundReport.Diagnosis,
                    GeneralCondition = foundReport.GeneralCondition,
                    Height = foundReport.Height,
                    MedicalHistory = foundReport.MedicalHistory,
                    Pulse = foundReport.Pulse,
                    ReportId = foundReport.Id,
                    Temperature = foundReport.Temperature,
                    Weight = foundReport.Weight,
                    HasFeedback = hasFeedback,
                    MedicineOrderId = foundReport.MedicineOrderId,
                    ServiceOrderId = foundReport.ServiceOrderId,
                },
                DoctorInfor = new GetUserMedicalReportResponse.Body.Doctor()
                {
                    DoctorAvatar = foundReport.Appointment.Schedule.Doctor.User.Avatar,
                    DoctorId = foundReport.Appointment.Schedule.Doctor.UserId,
                    DoctorName = foundReport.Appointment.Schedule.Doctor.User.FullName,
                    DoctorPosition = new GetUserMedicalReportResponse.Body.Doctor.Position()
                    {
                        PositionId = foundReport.Appointment.Schedule.Doctor.Position.Id,
                        PositionConstant = foundReport
                            .Appointment
                            .Schedule
                            .Doctor
                            .Position
                            .Constant,
                        PositionName = foundReport.Appointment.Schedule.Doctor.Position.Name,
                    },
                    DoctorSpecialties =
                        foundReport.Appointment.Schedule.Doctor.DoctorSpecialties.Select(
                            doctorSpecailty => new GetUserMedicalReportResponse.Body.Doctor.Specialty()
                            {
                                SpecialtyConstant = doctorSpecailty.Specialty.Constant,
                                SpecialtyId = doctorSpecailty.Specialty.Id,
                                SpecialtyName = doctorSpecailty.Specialty.Name,
                            }
                        ),
                },
            },
        };
    }
}
