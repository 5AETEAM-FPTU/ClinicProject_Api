using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Commons.Constance;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;

namespace Clinic.Application.Features.MedicalReports.CreateMedicalReport;

/// <summary>
///     CreateMedicalReport Handler.
/// </summary>
internal sealed class CreateMedicalReportHandler
    : IFeatureHandler<CreateMedicalReportRequest, CreateMedicalReportResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreateMedicalReportHandler(
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor
    )
    {
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
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
    public async Task<CreateMedicalReportResponse> ExecuteAsync(
        CreateMedicalReportRequest command,
        CancellationToken ct
    )
    {
        var role = _httpContextAccessor.HttpContext.User.FindFirstValue(claimType: "role");
        if (!(Equals(objA: role, objB: "doctor") || Equals(objA: role, objB: "staff")))
        {
            return new() { StatusCode = CreateMedicalReportResponseStatusCode.FORBIDEN_ACCESS };
        }

        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(claimType: "sub");

        // Find patient by id.
        var foundPatient =
            await _unitOfWork.CreateMedicalReportRepository.FindPatientByIdQueryAsync(
                patientId: command.PatientId,
                cancellationToken: ct
            );

        // Respond if patient id is not found.
        if (Equals(objA: foundPatient, objB: default))
        {
            return new()
            {
                StatusCode = CreateMedicalReportResponseStatusCode.PATIENT_IS_NOT_FOUND,
            };
        }

        // Is appointment id found.
        var isAppointmentFound =
            await _unitOfWork.CreateMedicalReportRepository.IsAppointmentFoundByIdQueryAsync(
                appointmentId: command.AppointmentId,
                cancellationToken: ct
            );

        // Repond if appointment id is not found.
        if (!isAppointmentFound)
        {
            return new()
            {
                StatusCode = CreateMedicalReportResponseStatusCode.APPOINTMENT_HAS_ALREADY_REPORT,
            };
        }

        // Is appointment reported.
        var isAppointmentReported =
            await _unitOfWork.CreateMedicalReportRepository.IsAppointmentReportedQueryAsync(
                appointmentId: command.AppointmentId,
                cancellationToken: ct
            );

        // Repond if appointments has been reported.
        if (isAppointmentReported)
        {
            return new()
            {
                StatusCode = CreateMedicalReportResponseStatusCode.APPOINTMENT_HAS_ALREADY_REPORT,
            };
        }

        // Init medical report.
        var newMedicalReport = InitMedicalReport(
            patient: foundPatient,
            createdBy: Guid.Parse(input: userId),
            appointmentId: command.AppointmentId,
            request: command
        );

        // Add medical report to database.
        var dbResult =
            await _unitOfWork.CreateMedicalReportRepository.CreateMedicalReportCommandAsync(
                newMedicalReport: newMedicalReport,
                cancellationToken: ct
            );

        // Respond if database operation fail.
        if (!dbResult)
        {
            return new()
            {
                StatusCode = CreateMedicalReportResponseStatusCode.DATABASE_OPERATION_FAIL,
            };
        }

        // Respond successfully.
        return new()
        {
            StatusCode = CreateMedicalReportResponseStatusCode.OPERATION_SUCCESS,
            Body = new() { MedicalReportId = newMedicalReport.Id },
        };
    }

    /// <summary>
    ///     Init medical report
    /// </summary>
    /// <param name="patient"></param>
    /// <param name="request"></param>
    /// <param name="createdBy"></param>
    /// <param name="appointmentId"></param>
    /// <returns>MedicalReport</returns>
    private static MedicalReport InitMedicalReport(
        User patient,
        CreateMedicalReportRequest request,
        Guid createdBy,
        Guid appointmentId
    )
    {
        return new()
        {
            AppointmentId = appointmentId,
            ServiceOrder = new()
            {
                Id = Guid.NewGuid(),
                IsAllUpdated = false,
                Quantity = 0,
                TotalPrice = 0,
            },
            PatientInformation = new PatientInformation()
            {
                Id = Guid.NewGuid(),
                FullName = patient.FullName,
                DOB = patient.Patient.DOB,
                Address = patient.Patient.Address,
                Gender = patient.Gender.Name,
                PhoneNumber = patient.PhoneNumber,
            },
            MedicineOrder = new() { Id = Guid.NewGuid(), TotalItem = 0 },
            Diagnosis = request.Diagnosis,
            MedicalHistory = request.MedicalHistory,
            GeneralCondition = request.GeneralCondition,
            Pulse = request.Pulse,
            Height = request.Height,
            BloodPresser = request.BloodPresser,
            Name = request.Name,
            Temperature = request.Temperature,
            Weight = request.Weight,
            CreatedAt = DateTime.Now,
            CreatedBy = createdBy,
            RemovedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
            TotalPrice = 0,
        };
    }
}
