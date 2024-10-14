using System;
using System.Collections.Generic;
using Clinic.Application.Features.Admin.DeleteMedicineTypeById;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Admin.DeleteMedicineTypeById.HttpResponseMapper;

/// <summary>
///     Mapper for DeleteMedicineTypeById feature
/// </summary>
public class DeleteMedicineTypeByIdHttpResponseManager
{
    private readonly Dictionary<
        DeleteMedicineTypeByIdResponseStatusCode,
        Func<
            DeleteMedicineTypeByIdRequest,
            DeleteMedicineTypeByIdResponse,
            DeleteMedicineTypeByIdHttpResponse
        >
    > _dictionary;

    internal DeleteMedicineTypeByIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: DeleteMedicineTypeByIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: DeleteMedicineTypeByIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: DeleteMedicineTypeByIdResponseStatusCode.FORBIDEN,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: DeleteMedicineTypeByIdResponseStatusCode.NOT_FOUND_MEDICINE_TYPE,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        DeleteMedicineTypeByIdRequest,
        DeleteMedicineTypeByIdResponse,
        DeleteMedicineTypeByIdHttpResponse
    > Resolve(DeleteMedicineTypeByIdResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
