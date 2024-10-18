using System;
using System.Collections.Generic;
using Clinic.Application.Features.Admin.DeleteMedicineGroupById;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Admin.DeleteMedicineGroupById.HttpResponseMapper;

/// <summary>
///     Mapper for DeleteMedicineGroupById feature
/// </summary>
public class DeleteMedicineGroupByIdHttpResponseManager
{
    private readonly Dictionary<
        DeleteMedicineGroupByIdResponseStatusCode,
        Func<
            DeleteMedicineGroupByIdRequest,
            DeleteMedicineGroupByIdResponse,
            DeleteMedicineGroupByIdHttpResponse
        >
    > _dictionary;

    internal DeleteMedicineGroupByIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: DeleteMedicineGroupByIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: DeleteMedicineGroupByIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: DeleteMedicineGroupByIdResponseStatusCode.FORBIDEN,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: DeleteMedicineGroupByIdResponseStatusCode.NOT_FOUND_MEDICINE_GROUP,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        DeleteMedicineGroupByIdRequest,
        DeleteMedicineGroupByIdResponse,
        DeleteMedicineGroupByIdHttpResponse
    > Resolve(DeleteMedicineGroupByIdResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
