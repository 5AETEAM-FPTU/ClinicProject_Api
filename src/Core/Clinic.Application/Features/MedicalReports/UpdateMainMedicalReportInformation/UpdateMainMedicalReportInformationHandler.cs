using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;

namespace Clinic.Application.Features.MedicalReports.UpdateMainMedicalReportInformation;

internal sealed class UpdateMainMedicalReportInformationHandler
    : IFeatureHandler<
        UpdateMainMedicalReportInformationRequest,
        UpdateMainMedicalReportInformationResponse
    >
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UpdateMainMedicalReportInformationHandler(
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor
    )
    {
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<UpdateMainMedicalReportInformationResponse> ExecuteAsync(
        UpdateMainMedicalReportInformationRequest command,
        CancellationToken ct
    )
    {
        var role = _httpContextAccessor.HttpContext.User.FindFirstValue(claimType: "role");
        if (Equals(objA: role, objB: "user"))
        {
            return new UpdateMainMedicalReportInformationResponse()
            {
                StatusCode = UpdateMainMedicalReportInformationResponseStatusCode.FORBIDDEN,
            };
        }

        var foundMedicalReport =
            await _unitOfWork.UpdateMainMedicalReportInformationRepository.GetMedicalReportsByIdQueryAsync(
                medicalReportId: command.ReportId,
                cancellationToken: ct
            );

        if (Equals(objA: foundMedicalReport, objB: default))
        {
            return new UpdateMainMedicalReportInformationResponse()
            {
                StatusCode = UpdateMainMedicalReportInformationResponseStatusCode.NOT_FOUND,
            };
        }

        var isUpdateSuccess = await UpdateMainMedicalReportInformationMethod(
            foundMedicalReport,
            command,
            ct
        );

        if (Equals(objA: isUpdateSuccess, objB: default))
        {
            return new UpdateMainMedicalReportInformationResponse()
            {
                StatusCode =
                    UpdateMainMedicalReportInformationResponseStatusCode.DATABASE_OPERATION_FAILED,
            };
        }

        return new UpdateMainMedicalReportInformationResponse()
        {
            StatusCode = UpdateMainMedicalReportInformationResponseStatusCode.OPERATION_SUCCESSFUL,
        };
    }

    private async Task<bool> UpdateMainMedicalReportInformationMethod(
        MedicalReport medicalReport,
        UpdateMainMedicalReportInformationRequest command,
        CancellationToken ct
    )
    {
        if (!Equals(objA: medicalReport, objB: default))
        {
            medicalReport.MedicalHistory = command.MedicalHistory;
            medicalReport.GeneralCondition = command.GeneralCondition;
            medicalReport.Weight = command.Weight;
            medicalReport.Height = command.Height;
            medicalReport.Pulse = command.Pulse;
            medicalReport.Temperature = command.Temperature;
            medicalReport.BloodPresser = command.BloodPresser;
            medicalReport.Diagnosis = command.Diagnosis;
        }
        return await _unitOfWork.UpdateMainMedicalReportInformationRepository.UpdateMainMedicalReportInformationCommandAsync(
            medicalReport: medicalReport,
            cancellationToken: ct
        );
    }
}
