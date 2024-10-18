using System.Collections.Generic;
using System;
using Clinic.Application.Features.Doctors.GetUserInforById;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Doctors.GetUserInforById.HttpResponseMapper;

public class GetUserInforByIdHttpResponseManager
{
    private readonly Dictionary<
        GetUserInforByIdResponseStatusCode,
        Func<
            GetUserInforByIdRequest,
            GetUserInforByIdResponse,
            GetUserInforByIdHttpResponse
        >
    > _dictionary;

    internal GetUserInforByIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetUserInforByIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody,
                }
        );

        _dictionary.Add(
            key: GetUserInforByIdResponseStatusCode.USER_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: GetUserInforByIdResponseStatusCode.FORBIDDEN,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        GetUserInforByIdRequest,
        GetUserInforByIdResponse,
        GetUserInforByIdHttpResponse
    > Resolve(GetUserInforByIdResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
