using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Http;
using Clinic.Application.Features.ExaminationServices.GetAvailableServices;

namespace Clinic.WebAPI.EndPoints.ExaminationServices.GetAvailableServices.HttpResponseMapper;

/// <summary>
///     GetAvailableSerivces response manager
/// </summary>
public class GetAvailableServicesHttpResponseManager
{
    private readonly Dictionary<
        GetAvailableServicesResponseStatusCode,
        Func<GetAvailableServicesRequest, GetAvailableServicesResponse, GetAvailableServicesHttpResponse>
    > _dictionary;

    internal GetAvailableServicesHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetAvailableServicesResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );

        _dictionary.Add(
            key: GetAvailableServicesResponseStatusCode.ROLE_IS_NOT_ADMIN_STAFF,
            value: (request, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        GetAvailableServicesRequest,
        GetAvailableServicesResponse,
        GetAvailableServicesHttpResponse
    > Resolve(GetAvailableServicesResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}

