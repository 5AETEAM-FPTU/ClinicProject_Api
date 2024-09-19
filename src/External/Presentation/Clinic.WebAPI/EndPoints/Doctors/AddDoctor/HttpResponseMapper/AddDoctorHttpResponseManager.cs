using System;
using System.Collections.Generic;
using Clinic.Application.Features.Auths.AddDoctor;
using Clinic.Application.Features.Doctors.AddDoctor;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Doctors.AddDoctor.HttpResponseMapper;

public class AddDoctorHttpResponseManager
{
    private readonly Dictionary<
        AddDoctorResponseStatusCode,
        Func<AddDoctorRequest, AddDoctorResponse, AddDoctorHttpResponse>
    > _dictionary;

    internal AddDoctorHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: AddDoctorResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: AddDoctorResponseStatusCode.USER_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: AddDoctorResponseStatusCode.USER_IS_TEMPORARILY_REMOVED,
            value: (request, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status417ExpectationFailed,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: AddDoctorResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: AddDoctorResponseStatusCode.DOCTOR_STAFF_TYPE_GUID_IS_NOT_EXIST,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: AddDoctorResponseStatusCode.FORBIDEN_ACCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: AddDoctorResponseStatusCode.EMAIL_DOCTOR_EXITS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );
    }

    internal Func<AddDoctorRequest, AddDoctorResponse, AddDoctorHttpResponse> Resolve(
        AddDoctorResponseStatusCode statusCode
    )
    {
        return _dictionary[statusCode];
    }
}
