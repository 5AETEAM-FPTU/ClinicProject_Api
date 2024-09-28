using System;
using System.Collections.Generic;
using Clinic.Application.Features.Doctors.UpdatePrivateDoctorInfo;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Doctors.UpdatePrivateDoctorInfo.HttpResponseMapper;

public class UpdatePrivateDoctorInfoHttpResponseManager
{
    private readonly Dictionary<
        UpdatePrivateDoctorInfoByIdResponseStatusCode,
        Func<
            UpdatePrivateDoctorInfoByIdRequest,
            UpdatePrivateDoctorInfoByIdResponse,
            UpdatePrivateDoctorInfoHttpResponse
        >
    > _dictionary;

    internal UpdatePrivateDoctorInfoHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: UpdatePrivateDoctorInfoByIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: UpdatePrivateDoctorInfoByIdResponseStatusCode.USER_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: UpdatePrivateDoctorInfoByIdResponseStatusCode.USER_IS_TEMPORARILY_REMOVED,
            value: (request, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status417ExpectationFailed,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: UpdatePrivateDoctorInfoByIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status417ExpectationFailed,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: UpdatePrivateDoctorInfoByIdResponseStatusCode.POSITION_ID_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: UpdatePrivateDoctorInfoByIdResponseStatusCode.SPECIALTY_ID_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: UpdatePrivateDoctorInfoByIdResponseStatusCode.GENDER_ID_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );
    }

    internal Func<
        UpdatePrivateDoctorInfoByIdRequest,
        UpdatePrivateDoctorInfoByIdResponse,
        UpdatePrivateDoctorInfoHttpResponse
    > Resolve(UpdatePrivateDoctorInfoByIdResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
