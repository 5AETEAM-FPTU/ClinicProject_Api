using System;
using System.Collections.Generic;
using Clinic.Application.Commons.Abstractions.GetProfileUser;
using Clinic.Application.Features.Doctors.GetIdsDoctor;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Doctors.GetIdsDoctor.HttpResponseMapper;

/// <summary>
///     Class GetIdsDoctorHttpResponseManager.
/// </summary>
public class GetIdsDoctorHttpResponseManager
{
    private readonly Dictionary<
        GetIdsDoctorResponseStatusCode,
        Func<GetIdsDoctorRequest, GetIdsDoctorResponse, GetIdsDoctorHttpResponse>
    > _dictionary;

    internal GetIdsDoctorHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetIdsDoctorResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );
    }

    internal Func<GetIdsDoctorRequest, GetIdsDoctorResponse, GetIdsDoctorHttpResponse> Resolve(
        GetIdsDoctorResponseStatusCode statusCode
    )
    {
        return _dictionary[statusCode];
    }
}
