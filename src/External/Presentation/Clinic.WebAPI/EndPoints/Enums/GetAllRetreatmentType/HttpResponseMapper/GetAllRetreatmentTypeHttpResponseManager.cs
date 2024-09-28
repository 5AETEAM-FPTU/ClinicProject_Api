using System.Collections.Generic;
using System;
using Clinic.Application.Features.Enums.GetAllRetreatmentType;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Enums.GetAllRetreatmentType.HttpResponseMapper;

/// <summary>
///     Mapper for GetAllRetreatmentType feature
/// </summary>
public class GetAllRetreatmentTypeHttpResponseManager
{
    private readonly Dictionary<
        GetAllRetreatmentTypeResponseStatusCode,
        Func<GetAllRetreatmentTypeRequest, GetAllRetreatmentTypeResponse, GetAllRetreatmentTypeHttpResponse>
    > _dictionary;

    internal GetAllRetreatmentTypeHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetAllRetreatmentTypeResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );

    }

    internal Func<GetAllRetreatmentTypeRequest, GetAllRetreatmentTypeResponse, GetAllRetreatmentTypeHttpResponse> Resolve(
        GetAllRetreatmentTypeResponseStatusCode statusCode
    )
    {
        return _dictionary[statusCode];
    }
}