using System;
using System.Collections.Generic;
using Clinic.Application.Features.MedicalReports.CreateMedicalReport;
using Clinic.Application.Features.MedicalReports.UpdateMedicalReportPatientInformation;
using Clinic.WebAPI.EndPoints.MedicalReports.CreateMedicalReport.HttpResponseMapper;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.MedicalReports.UpdatePatientInformation.HttpResponseMapper;

public class UpdatePatientInformationHttpResponseManager
{
    private readonly Dictionary<
        UpdateMedicalReportPatientInformationResponseStatusCode,
        Func<
            UpdateMedicalReportPatientInformationRequest,
            UpdateMedicalReportPatientInformationResponse,
            UpdatePatientInformationHttpResponse
        >
    > _dictionary;

    internal UpdatePatientInformationHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: UpdateMedicalReportPatientInformationResponseStatusCode.OPERATION_SUCCESSFUL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
        _dictionary.Add(
            key: UpdateMedicalReportPatientInformationResponseStatusCode.DATABASE_OPERATION_FAILED,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
        _dictionary.Add(
            key: UpdateMedicalReportPatientInformationResponseStatusCode.UNAUTHORIZED,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status401Unauthorized,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
        _dictionary.Add(
            key: UpdateMedicalReportPatientInformationResponseStatusCode.FORBIDDEN,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
        _dictionary.Add(
            key: UpdateMedicalReportPatientInformationResponseStatusCode.NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        UpdateMedicalReportPatientInformationRequest,
        UpdateMedicalReportPatientInformationResponse,
        UpdatePatientInformationHttpResponse
    > Resolve(UpdateMedicalReportPatientInformationResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
