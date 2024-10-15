using System.Collections.Generic;
using System;
using Clinic.Application.Features.Admin.UpdateMedicineTypeById;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Admin.UpdateMedicineTypeById.HttpResponseMapper;

public class UpdateMedicineTypeByIdHttpResponseManager
{
    private readonly Dictionary<
        UpdateMedicineTypeByIdResponseStatusCode,
        Func<UpdateMedicineTypeByIdRequest, UpdateMedicineTypeByIdResponse, UpdateMedicineTypeByIdHttpResponse>
    > _dictionary;

    internal UpdateMedicineTypeByIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: UpdateMedicineTypeByIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
           key: UpdateMedicineTypeByIdResponseStatusCode.MEDICINE_TYPE_IS_NOT_FOUND,
           value: (_, response) =>
               new()
               {
                   HttpCode = StatusCodes.Status404NotFound,
                   AppCode = response.StatusCode.ToAppCode()
               }
       );

        _dictionary.Add(
           key: UpdateMedicineTypeByIdResponseStatusCode.DATABASE_OPERATION_FAIL,
           value: (_, response) =>
               new()
               {
                   HttpCode = StatusCodes.Status417ExpectationFailed,
                   AppCode = response.StatusCode.ToAppCode()
               }
       );

        _dictionary.Add(
            key: UpdateMedicineTypeByIdResponseStatusCode.FORBIDEN_ACCESS,
            value: (request, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

    }

    internal Func<
        UpdateMedicineTypeByIdRequest,
        UpdateMedicineTypeByIdResponse,
        UpdateMedicineTypeByIdHttpResponse
    > Resolve(UpdateMedicineTypeByIdResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
