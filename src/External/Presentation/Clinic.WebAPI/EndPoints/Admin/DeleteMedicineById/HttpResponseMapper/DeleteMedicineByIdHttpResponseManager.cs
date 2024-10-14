using System.Collections.Generic;
using System;
using Clinic.Application.Features.Admin.DeleteMedicineById;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Admin.DeleteMedicineById.HttpResponseMapper;

/// <summary>
///     Mapper for DeleteMedicineById feature
/// </summary>
public class DeleteMedicineByIdHttpResponseManager
{
    private readonly Dictionary<
        DeleteMedicineByIdResponseStatusCode,
        Func<DeleteMedicineByIdRequest, DeleteMedicineByIdResponse, DeleteMedicineByIdHttpResponse>
    > _dictionary;

    internal DeleteMedicineByIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: DeleteMedicineByIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: DeleteMedicineByIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: DeleteMedicineByIdResponseStatusCode.FORBIDEN,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );


        _dictionary.Add(
            key: DeleteMedicineByIdResponseStatusCode.NOT_FOUND_MEDICINE,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        DeleteMedicineByIdRequest,
        DeleteMedicineByIdResponse,
        DeleteMedicineByIdHttpResponse
    > Resolve(DeleteMedicineByIdResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}

