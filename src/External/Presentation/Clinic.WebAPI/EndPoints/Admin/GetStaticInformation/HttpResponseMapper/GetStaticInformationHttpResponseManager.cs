using System.Collections.Generic;
using System;
using Clinic.Application.Features.Admin.GetStaticInformation;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Admin.GetStaticInformation.HttpResponseMapper;

public class GetStaticInformationHttpResponseManager
{
    private readonly Dictionary<
       GetStaticInformationResponseStatusCode,
       Func<
           GetStaticInformationRequest,
           GetStaticInformationResponse,
           GetStaticInformationHttpResponse
       >
   > _dictionary;

    internal GetStaticInformationHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetStaticInformationResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody,
                }
        );
        _dictionary.Add(
            key: GetStaticInformationResponseStatusCode.OPERATION_FAILED,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: GetStaticInformationResponseStatusCode.USER_IS_NOT_ADMIN,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        GetStaticInformationRequest,
        GetStaticInformationResponse,
        GetStaticInformationHttpResponse
    > Resolve(GetStaticInformationResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
