using System;
using System.Collections.Generic;
using Clinic.Application.Features.Admin.DeleteMedicineTypeById;
using Clinic.Application.Features.Admin.GetMedicineTypeById;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Admin.GetMedicineTypeById.HttpResponseMapper;

public class GetMedicineTypeByIdHttpResponseManager
{
    private readonly Dictionary<
        GetMedicineTypeByIdResponseStatusCode,
        Func<
            GetMedicineTypeByIdRequest,
            GetMedicineTypeByIdResponse,
            GetMedicineTypeByIdHttpResponse
        >
    > _dictionary;

    internal GetMedicineTypeByIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetMedicineTypeByIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody,
                }
        );
        _dictionary.Add(
            key: GetMedicineTypeByIdResponseStatusCode.MEDICINE_TYPE_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        GetMedicineTypeByIdRequest,
        GetMedicineTypeByIdResponse,
        GetMedicineTypeByIdHttpResponse
    > Resolve(GetMedicineTypeByIdResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
