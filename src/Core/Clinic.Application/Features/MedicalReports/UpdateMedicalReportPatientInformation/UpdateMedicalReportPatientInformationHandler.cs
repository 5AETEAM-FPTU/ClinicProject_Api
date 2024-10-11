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

namespace Clinic.Application.Features.MedicalReports.UpdateMedicalReportPatientInformation;

internal sealed class UpdateMedicalReportPatientInformationHandler
    : IFeatureHandler<
        UpdateMedicalReportPatientInformationRequest,
        UpdateMedicalReportPatientInformationResponse
    >
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UpdateMedicalReportPatientInformationHandler(
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor
    )
    {
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<UpdateMedicalReportPatientInformationResponse> ExecuteAsync(
        UpdateMedicalReportPatientInformationRequest command,
        CancellationToken ct
    )
    {
        var role = _httpContextAccessor.HttpContext.User.FindFirstValue(claimType: "role");

        if (Equals(objA: role, objB: "user"))
        {
            return new UpdateMedicalReportPatientInformationResponse()
            {
                StatusCode = UpdateMedicalReportPatientInformationResponseStatusCode.FORBIDDEN,
            };
        }

        var foundPatientInformation =
            await _unitOfWork.UpdateMedicalReportPatientInformationRepository.FindPatientInformationByIdQueryAsync(
                command.PatientId,
                ct
            );
        if (foundPatientInformation is null)
        {
            return new UpdateMedicalReportPatientInformationResponse()
            {
                StatusCode = UpdateMedicalReportPatientInformationResponseStatusCode.NOT_FOUND,
            };
        }

        var dbResult = await UpdateMedicalReportPatientInformationExtentionMethod(
            foundPatientInformation,
            command,
            ct
        );

        if (Equals(objA: dbResult, objB: false))
        {
            return new UpdateMedicalReportPatientInformationResponse()
            {
                StatusCode =
                    UpdateMedicalReportPatientInformationResponseStatusCode.DATABASE_OPERATION_FAILED,
            };
        }

        return new UpdateMedicalReportPatientInformationResponse()
        {
            StatusCode =
                UpdateMedicalReportPatientInformationResponseStatusCode.OPERATION_SUCCESSFUL,
        };
    }

    private async Task<bool> UpdateMedicalReportPatientInformationExtentionMethod(
        PatientInformation patientInformation,
        UpdateMedicalReportPatientInformationRequest command,
        CancellationToken ct
    )
    {
        if (patientInformation != null)
        {
            patientInformation.Address = command.Address;
            patientInformation.PhoneNumber = command.PhoneNumber;
            patientInformation.FullName = command.FullName;
            patientInformation.DOB = command.Dob;
            patientInformation.Gender = command.Gender;
        }

        return await _unitOfWork.UpdateMedicalReportPatientInformationRepository.UpdatePatientInformationCommandAsync(
            patientInformation,
            ct
        );
    }
}
