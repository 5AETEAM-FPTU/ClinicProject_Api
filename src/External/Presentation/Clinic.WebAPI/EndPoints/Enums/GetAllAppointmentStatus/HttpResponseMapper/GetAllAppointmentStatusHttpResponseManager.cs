

using System.Collections.Generic;
using System;
using Clinic.Application.Features.Enums.GetAllAppointmentStatus;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Enums.GetAllAppointmentStatus.HttpResponseMapper;

/// <summary>
///     Mapper for GetAllAppointmentStatus feature
/// </summary>
public class GetAllAppointmentStatusHttpResponseManager
{
    private readonly Dictionary<
        GetAllAppointmentStatusResponseStatusCode,
        Func<GetAllAppointmentStatusRequest, GetAllAppointmentStatusResponse, GetAllAppointmentStatusHttpResponse>
    > _dictionary;

    internal GetAllAppointmentStatusHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetAllAppointmentStatusResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );

    }

    internal Func<GetAllAppointmentStatusRequest, GetAllAppointmentStatusResponse, GetAllAppointmentStatusHttpResponse> Resolve(
        GetAllAppointmentStatusResponseStatusCode statusCode
    )
    {
        return _dictionary[statusCode];
    }
}
