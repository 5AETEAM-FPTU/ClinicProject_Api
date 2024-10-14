using System.Collections.Generic;
using System;
using Clinic.Application.Features.Admin.CreateNewMedicineType;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Admin.CreateNewMedicineType.HttpResponseMapper;

/// <summary>
///     Mapper for CreateNewMedicineType feature
/// </summary>
public class CreateNewMedicineTypeHttpResponseManager
{
    private readonly Dictionary<
        CreateNewMedicineTypeResponseStatusCode,
        Func<CreateNewMedicineTypeRequest, CreateNewMedicineTypeResponse, CreateNewMedicineTypeHttpResponse>
    > _dictionary;

    internal CreateNewMedicineTypeHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: CreateNewMedicineTypeResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: CreateNewMedicineTypeResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: CreateNewMedicineTypeResponseStatusCode.FORBIDEN_ACCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: CreateNewMedicineTypeResponseStatusCode.MEDICINE_TYPE_ALREADY_EXISTED,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        CreateNewMedicineTypeRequest,
        CreateNewMedicineTypeResponse,
        CreateNewMedicineTypeHttpResponse
    > Resolve(CreateNewMedicineTypeResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}

