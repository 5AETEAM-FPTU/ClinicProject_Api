using System.Collections.Generic;
using System;
using Clinic.Application.Features.Admin.GetAllMedicineGroup;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Admin.GetAllMedicineGroup.HttpResponseMapper;

/// <summary>
///     Mapper for GetAllMedicineGroup feature
/// </summary>
public class GetAllMedicineGroupHttpResponseManager
{
    private readonly Dictionary<
        GetAllMedicineGroupResponseStatusCode,
        Func<GetAllMedicineGroupRequest, GetAllMedicineGroupResponse, GetAllMedicineGroupHttpResponse>
    > _dictionary;

    internal GetAllMedicineGroupHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetAllMedicineGroupResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );

    }

    internal Func<GetAllMedicineGroupRequest, GetAllMedicineGroupResponse, GetAllMedicineGroupHttpResponse> Resolve(
        GetAllMedicineGroupResponseStatusCode statusCode
    )
    {
        return _dictionary[statusCode];
    }
}

