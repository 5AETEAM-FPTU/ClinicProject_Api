using System.Collections.Generic;
using System;
using Clinic.Application.Features.Enums.GetAllGender;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Enums.GetAllGender.HttpResponseMapper;

/// <summary>
///     Mapper for GetAllGender feature
/// </summary>
public class GetAllGenderHttpResponseManager
{
    private readonly Dictionary<
        GetAllGenderResponseStatusCode,
        Func<GetAllGenderRequest, GetAllGenderResponse, GetAllGenderHttpResponse>
    > _dictionary;

    internal GetAllGenderHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetAllGenderResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );

    }

    internal Func<GetAllGenderRequest, GetAllGenderResponse, GetAllGenderHttpResponse> Resolve(
        GetAllGenderResponseStatusCode statusCode
    )
    {
        return _dictionary[statusCode];
    }
}