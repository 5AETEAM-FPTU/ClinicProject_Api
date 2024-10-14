using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Http;
using Clinic.Application.Features.ExaminationServices.GetDetailService;

namespace Clinic.WebAPI.EndPoints.ExaminationServices.GetDetailService.HttpResponseMapper;

public class GetDetailServiceHttpResponseManager
{
    private readonly Dictionary<
        GetDetailServiceResponseStatusCode,
        Func<GetDetailServiceRequest, GetDetailServiceResponse, GetDetailServiceHttpResponse>
    > _dictionary;

    internal GetDetailServiceHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetDetailServiceResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );


        _dictionary.Add(
            key: GetDetailServiceResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );


        _dictionary.Add(
            key: GetDetailServiceResponseStatusCode.SERVICE_NOT_FOUND,
            value: (request, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        GetDetailServiceRequest,
        GetDetailServiceResponse,
        GetDetailServiceHttpResponse
    > Resolve(GetDetailServiceResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
