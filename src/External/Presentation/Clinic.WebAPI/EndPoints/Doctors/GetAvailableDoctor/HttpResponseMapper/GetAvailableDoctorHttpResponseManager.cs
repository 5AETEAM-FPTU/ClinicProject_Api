using System;
using System.Collections.Generic;
using Clinic.Application.Commons.Abstractions.GetProfileUser;
using Clinic.Application.Features.Doctors.GetAvailableDoctor;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Doctors.GetAvailableDoctor.HttpResponseMapper;

public class GetAvailableDoctorHttpResponseManager
{
    private readonly Dictionary<
        GetAvailableDoctorResponseStatusCode,
        Func<GetAvailableDoctorRequest, GetAvailableDoctorResponse, GetAvailableDoctorHttpResponse>
    > _dictionary;

    internal GetAvailableDoctorHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetAvailableDoctorResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );
    }

    internal Func<
        GetAvailableDoctorRequest,
        GetAvailableDoctorResponse,
        GetAvailableDoctorHttpResponse
    > Resolve(GetAvailableDoctorResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
