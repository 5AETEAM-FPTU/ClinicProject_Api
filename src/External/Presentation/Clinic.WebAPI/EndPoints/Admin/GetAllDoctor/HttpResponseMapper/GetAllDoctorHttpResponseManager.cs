using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Http;
using Clinic.Application.Features.Admin.GetAllDoctor;

namespace Clinic.WebAPI.EndPoints.Admin.GetAllDoctor.HttpResponseMapper;

/// <summary>
///     Mapper for GetAllDoctors feature
/// </summary>
public class GetAllDoctorHttpResponseManager
{
    private readonly Dictionary<
        GetAllDoctorResponseStatusCode,
        Func<GetAllDoctorRequest, GetAllDoctorResponse, GetAllDoctorHttpResponse>
    > _dictionary;

    internal GetAllDoctorHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetAllDoctorResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );

        _dictionary.Add(
            key: GetAllDoctorResponseStatusCode.ROLE_IS_NOT_ADMIN,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );

    }

    internal Func<GetAllDoctorRequest, GetAllDoctorResponse, GetAllDoctorHttpResponse> Resolve(
        GetAllDoctorResponseStatusCode statusCode
    )
    {
        return _dictionary[statusCode];
    }
}

