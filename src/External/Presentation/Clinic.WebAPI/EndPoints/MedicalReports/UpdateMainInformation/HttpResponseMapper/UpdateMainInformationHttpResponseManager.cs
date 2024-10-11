using System;
using System.Collections.Generic;
using Clinic.Application.Features.MedicalReports.CreateMedicalReport;
using Clinic.Application.Features.MedicalReports.UpdateMainMedicalReportInformation;
using Clinic.WebAPI.EndPoints.MedicalReports.CreateMedicalReport.HttpResponseMapper;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.MedicalReports.UpdateMainInformation.HttpResponseMapper;

public class UpdateMainInformationHttpResponseManager
{
    private readonly Dictionary<
        UpdateMainMedicalReportInformationResponseStatusCode,
        Func<
            UpdateMainMedicalReportInformationRequest,
            UpdateMainMedicalReportInformationResponse,
            UpdateMainInformationHttpResponse
        >
    > _dictionary;

    internal UpdateMainInformationHttpResponseManager()
    {
        _dictionary = [];
        _dictionary.Add(
            key: UpdateMainMedicalReportInformationResponseStatusCode.OPERATION_SUCCESSFUL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
        _dictionary.Add(
            key: UpdateMainMedicalReportInformationResponseStatusCode.DATABASE_OPERATION_FAILED,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
        _dictionary.Add(
            key: UpdateMainMedicalReportInformationResponseStatusCode.UNAUTHORIZED,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status401Unauthorized,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
        _dictionary.Add(
            key: UpdateMainMedicalReportInformationResponseStatusCode.FORBIDDEN,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
        _dictionary.Add(
            key: UpdateMainMedicalReportInformationResponseStatusCode.NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        UpdateMainMedicalReportInformationRequest,
        UpdateMainMedicalReportInformationResponse,
        UpdateMainInformationHttpResponse
    > Resolve(UpdateMainMedicalReportInformationResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
