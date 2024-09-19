using System;
using System.Collections.Generic;
using Clinic.Application.Commons.Abstractions.GetProfileUser;
using Clinic.Application.Features.Doctors.GetProfileDoctor;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Doctors.GetProfileDoctor.HttpResponseMapper;

public class GetProfileDoctorHttpResponseManager
{
    private readonly Dictionary<
        GetProfileDoctorResponseStatusCode,
        Func<GetProfileDoctorRequest, GetProfileDoctorResponse, GetProfileDoctorHttpResponse>
    > _dictionary;

    internal GetProfileDoctorHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetProfileDoctorResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );

        _dictionary.Add(
            key: GetProfileDoctorResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: GetProfileDoctorResponseStatusCode.USER_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: GetProfileDoctorResponseStatusCode.USER_IS_TEMPORARILY_REMOVED,
            value: (request, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status417ExpectationFailed,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: GetProfileDoctorResponseStatusCode.ROLE_IS_NOT_A_DOCTOR,
            value: (request, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        GetProfileDoctorRequest,
        GetProfileDoctorResponse,
        GetProfileDoctorHttpResponse
    > Resolve(GetProfileDoctorResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
