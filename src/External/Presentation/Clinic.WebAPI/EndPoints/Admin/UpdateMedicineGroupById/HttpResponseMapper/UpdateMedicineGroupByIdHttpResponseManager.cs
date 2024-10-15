using System.Collections.Generic;
using System;
using Clinic.Application.Features.Admin.UpdateMedicineGroupById;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Admin.UpdateMedicineGroupById.HttpResponseMapper;

public class UpdateMedicineGroupByIdHttpResponseManager
{
    private readonly Dictionary<
        UpdateMedicineGroupByIdResponseStatusCode,
        Func<UpdateMedicineGroupByIdRequest, UpdateMedicineGroupByIdResponse, UpdateMedicineGroupByIdHttpResponse>
    > _dictionary;

    internal UpdateMedicineGroupByIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: UpdateMedicineGroupByIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
           key: UpdateMedicineGroupByIdResponseStatusCode.MEDICINE_GROUP_IS_NOT_FOUND,
           value: (_, response) =>
               new()
               {
                   HttpCode = StatusCodes.Status404NotFound,
                   AppCode = response.StatusCode.ToAppCode()
               }
       );

        _dictionary.Add(
           key: UpdateMedicineGroupByIdResponseStatusCode.DATABASE_OPERATION_FAIL,
           value: (_, response) =>
               new()
               {
                   HttpCode = StatusCodes.Status417ExpectationFailed,
                   AppCode = response.StatusCode.ToAppCode()
               }
       );

        _dictionary.Add(
            key: UpdateMedicineGroupByIdResponseStatusCode.FORBIDEN_ACCESS,
            value: (request, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

    }

    internal Func<
        UpdateMedicineGroupByIdRequest,
        UpdateMedicineGroupByIdResponse,
        UpdateMedicineGroupByIdHttpResponse
    > Resolve(UpdateMedicineGroupByIdResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}