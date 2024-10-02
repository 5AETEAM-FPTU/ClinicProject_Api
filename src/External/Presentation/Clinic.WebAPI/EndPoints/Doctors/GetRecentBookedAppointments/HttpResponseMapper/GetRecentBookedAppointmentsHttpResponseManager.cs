using System;
using System.Collections.Generic;
using Clinic.Application.Features.Doctors.GetRecentBookedAppointments;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Doctors.GetRecentBookedAppointments.HttpResponseMapper;

/// <summary>
///     Mapper for GetSchedulesByDate feature
/// </summary>
public class GetRecentBookedAppointmentsHttpResponseManager
{
    private readonly Dictionary<
        GetRecentBookedAppointmentsResponseStatusCode,
        Func<GetRecentBookedAppointmentsRequest, GetRecentBookedAppointmentsResponse, GetRecentBookedAppointmentsHttpResponse>
    > _dictionary;

    internal GetRecentBookedAppointmentsHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetRecentBookedAppointmentsResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );

        _dictionary.Add(
            key: GetRecentBookedAppointmentsResponseStatusCode.USER_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );
    }

    internal Func<
        GetRecentBookedAppointmentsRequest,
        GetRecentBookedAppointmentsResponse,
        GetRecentBookedAppointmentsHttpResponse
    > Resolve(GetRecentBookedAppointmentsResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
