using System.Collections.Generic;
using System;
using Clinic.Application.Features.Admin.GetMedicineGroupById;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Admin.GetMedicineGroupById.HttpResponseMapper;

public class GetMedicineGroupByIdHttpResponseManager
{
    private readonly Dictionary<
        GetMedicineGroupByIdResponseStatusCode,
        Func<
            GetMedicineGroupByIdRequest,
            GetMedicineGroupByIdResponse,
            GetMedicineGroupByIdHttpResponse
        >
    > _dictionary;

    internal GetMedicineGroupByIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetMedicineGroupByIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody,
                }
        );
        _dictionary.Add(
            key: GetMedicineGroupByIdResponseStatusCode.MEDICINE_GROUP_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        GetMedicineGroupByIdRequest,
        GetMedicineGroupByIdResponse,
        GetMedicineGroupByIdHttpResponse
    > Resolve(GetMedicineGroupByIdResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
