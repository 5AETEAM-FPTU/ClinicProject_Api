using Clinic.Application.Features.Doctors.UpdateDoctorDescription;
using Clinic.WebAPI.EndPoints.Doctors.UpdateDoctorDescription.HttpResponseMapper;
using System.Collections.Generic;
using System;
using Clinic.Application.Features.Doctors.UpdateDutyStatus;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Doctors.UpdateDutyStatus.HttpResponseMapper;

public class UpdateDutyStatusHttpResponseManager
{
    private readonly Dictionary<
    UpdateDutyStatusResponseStatusCode,
    Func<UpdateDutyStatusRequest, UpdateDutyStatusResponse, UpdateDutyStatusHttpResponse>> _dictionary;

    internal UpdateDutyStatusHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: UpdateDutyStatusResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: UpdateDutyStatusResponseStatusCode.USER_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: UpdateDutyStatusResponseStatusCode.USER_IS_TEMPORARILY_REMOVED,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: UpdateDutyStatusResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        
    }

    internal Func<
        UpdateDutyStatusRequest,
        UpdateDutyStatusResponse,
        UpdateDutyStatusHttpResponse
    > Resolve(UpdateDutyStatusResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
