using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Http;
using Clinic.Application.Features.Doctors.UpdatePrivateDoctorInfo;
using Clinic.Application.Features.Users.UpdateUserPrivateInfo;


namespace Clinic.WebAPI.EndPoints.Doctors.UpdateUserPrivateInfo.HttpResponseMapper;

public class UpdateUserPrivateInfoHttpResponseManager
{
    private readonly Dictionary<
        UpdateUserPrivateInfoResponseStatusCode,
        Func<UpdateUserPrivateInfoRequest, UpdateUserPrivateInfoResponse, UpdateUserPrivateInfoHttpResponse>
    > _dictionary;

    internal UpdateUserPrivateInfoHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: UpdateUserPrivateInfoResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: UpdateUserPrivateInfoResponseStatusCode.USER_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: UpdateUserPrivateInfoResponseStatusCode.USER_IS_TEMPORARILY_REMOVED,
            value: (request, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status417ExpectationFailed,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: UpdateUserPrivateInfoResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status417ExpectationFailed,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

    }

    internal Func<
        UpdateUserPrivateInfoRequest,
        UpdateUserPrivateInfoResponse,
        UpdateUserPrivateInfoHttpResponse
    > Resolve(UpdateUserPrivateInfoResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
