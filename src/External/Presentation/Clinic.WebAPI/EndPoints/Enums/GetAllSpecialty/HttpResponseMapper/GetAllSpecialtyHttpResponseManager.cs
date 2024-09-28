using System;
using System.Collections.Generic;
using Clinic.Application.Features.Enums.GetAllSpecialty;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Enums.GetAllSpecialty.HttpResponseMapper;

/// <summary>
///     Mapper for GetAllSpecialty feature
/// </summary>
public class GetAllSpecialtyHttpResponseManager
{
    private readonly Dictionary<
        GetAllSpecialtyResponseStatusCode,
        Func<GetAllSpecialtyRequest, GetAllSpecialtyResponse, GetAllSpecialtyHttpResponse>
    > _dictionary;

    internal GetAllSpecialtyHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetAllSpecialtyResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );
    }

    internal Func<
        GetAllSpecialtyRequest,
        GetAllSpecialtyResponse,
        GetAllSpecialtyHttpResponse
    > Resolve(GetAllSpecialtyResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
