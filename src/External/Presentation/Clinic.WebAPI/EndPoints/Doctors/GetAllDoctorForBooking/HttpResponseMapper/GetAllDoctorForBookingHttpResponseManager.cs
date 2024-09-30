using System.Collections.Generic;
using System;
using Clinic.Application.Features.Doctors.GetAllDoctorForBooking;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Doctors.GetAllDoctorForBooking.HttpResponseMapper;

public class GetAllDoctorForBookingHttpResponseManager
{
    private readonly Dictionary<
        GetAllDoctorForBookingResponseStatusCode,
        Func<GetAllDoctorForBookingRequest, GetAllDoctorForBookingResponse, GetAllDoctorForBookingHttpResponse>
    > _dictionary;

    internal GetAllDoctorForBookingHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetAllDoctorForBookingResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );



        _dictionary.Add(
            key: GetAllDoctorForBookingResponseStatusCode.ROLE_IS_NOT_USER,
            value: (request, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        GetAllDoctorForBookingRequest,
        GetAllDoctorForBookingResponse,
        GetAllDoctorForBookingHttpResponse
    > Resolve(GetAllDoctorForBookingResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
