using System;
using System.Collections.Generic;
using Clinic.Application.Features.Appointments.GetAbsentForStaff;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Appointments.GetAbsentForStaff.HttpResponseMapper;

public class GetAbsentForStaffHttpResponseManager
{
    private readonly Dictionary<
        GetAbsentForStaffResponseStatusCode,
        Func<GetAbsentForStaffRequest, GetAbsentForStaffResponse, GetAbsentForStaffHttpResponse>
    > _dictionary;

    internal GetAbsentForStaffHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetAbsentForStaffResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );

        _dictionary.Add(
            key: GetAbsentForStaffResponseStatusCode.ROLE_IS_NOT_DOCTOR,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        GetAbsentForStaffRequest,
        GetAbsentForStaffResponse,
        GetAbsentForStaffHttpResponse
    > Resolve(GetAbsentForStaffResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
