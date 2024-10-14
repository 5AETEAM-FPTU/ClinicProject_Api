using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Http;
using Clinic.Application.Features.ExaminationServices.GetAllServices;

namespace Clinic.WebAPI.EndPoints.ExaminationServices.GetAllServices.HttpResponseMapper;

/// <summary>
///     GetAllSerivces response manager
/// </summary>
public class GetAllServicesHttpResponseManager
{
    private readonly Dictionary<
        GetAllServicesResponseStatusCode,
        Func<GetAllServicesRequest, GetAllServicesResponse, GetAllServicesHttpResponse>
    > _dictionary;

    internal GetAllServicesHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetAllServicesResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );

        _dictionary.Add(
            key: GetAllServicesResponseStatusCode.ROLE_IS_NOT_ADMIN_STAFF,
            value: (request, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        GetAllServicesRequest,
        GetAllServicesResponse,
        GetAllServicesHttpResponse
    > Resolve(GetAllServicesResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}

