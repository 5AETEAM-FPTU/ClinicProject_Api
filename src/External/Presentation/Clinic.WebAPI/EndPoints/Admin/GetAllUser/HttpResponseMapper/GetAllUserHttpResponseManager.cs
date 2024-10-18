using System;
using System.Collections.Generic;
using Clinic.Application.Features.Admin.GetAllUser;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Admin.GetAllUser.HttpResponseMapper;

/// <summary>
///     Mapper for GetAllDoctors feature
/// </summary>
public class GetAllUserHttpResponseManager
{
    private readonly Dictionary<
        GetAllUserResponseStatusCode,
        Func<GetAllUserRequest, GetAllUserResponse, GetAllUserHttpResponse>
    > _dictionary;

    internal GetAllUserHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetAllUserResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody,
                }
        );

        _dictionary.Add(
            key: GetAllUserResponseStatusCode.ROLE_IS_NOT_ADMIN,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody,
                }
        );
    }

    internal Func<GetAllUserRequest, GetAllUserResponse, GetAllUserHttpResponse> Resolve(
        GetAllUserResponseStatusCode statusCode
    )
    {
        return _dictionary[statusCode];
    }
}
