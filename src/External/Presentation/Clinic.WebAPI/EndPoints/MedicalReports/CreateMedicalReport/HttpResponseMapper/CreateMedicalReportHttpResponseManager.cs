using System;
using System.Collections.Generic;
using Clinic.Application.Features.MedicalReports.CreateMedicalReport;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.MedicalReports.CreateMedicalReport.HttpResponseMapper;

/// <summary>
///     Mapper for <see cref="CreateNewCreateMedicalReportResponse"/>
/// </summary>
public class CreateMedicalReportHttpResponseManager
{
    private readonly Dictionary<
        CreateMedicalReportResponseStatusCode,
        Func<
            CreateMedicalReportRequest,
            CreateMedicalReportResponse,
            CreateMedicalReportHttpResponse
        >
    > _dictionary;

    internal CreateMedicalReportHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: CreateMedicalReportResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
        _dictionary.Add(
            key: CreateMedicalReportResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: CreateMedicalReportResponseStatusCode.FORBIDEN_ACCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
        _dictionary.Add(
            key: CreateMedicalReportResponseStatusCode.PATIENT_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
        _dictionary.Add(
            key: CreateMedicalReportResponseStatusCode.APPOINTMENT_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
        _dictionary.Add(
            key: CreateMedicalReportResponseStatusCode.APPOINTMENT_HAS_ALREADY_REPORT,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        CreateMedicalReportRequest,
        CreateMedicalReportResponse,
        CreateMedicalReportHttpResponse
    > Resolve(CreateMedicalReportResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
