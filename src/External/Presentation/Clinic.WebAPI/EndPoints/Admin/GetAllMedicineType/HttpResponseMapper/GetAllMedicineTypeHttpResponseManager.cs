using System.Collections.Generic;
using System;
using Clinic.Application.Features.Admin.GetAllMedicineType;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Admin.GetAllMedicineType.HttpResponseMapper;

/// <summary>
///     Mapper for GetAllMedicineType feature
/// </summary>
public class GetAllMedicineTypeHttpResponseManager
{
    private readonly Dictionary<
        GetAllMedicineTypeResponseStatusCode,
        Func<GetAllMedicineTypeRequest, GetAllMedicineTypeResponse, GetAllMedicineTypeHttpResponse>
    > _dictionary;

    internal GetAllMedicineTypeHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetAllMedicineTypeResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );

    }

    internal Func<GetAllMedicineTypeRequest, GetAllMedicineTypeResponse, GetAllMedicineTypeHttpResponse> Resolve(
        GetAllMedicineTypeResponseStatusCode statusCode
    )
    {
        return _dictionary[statusCode];
    }
}
