using System.Collections.Generic;
using System;
using Clinic.Application.Features.Admin.CreateMedicine;
using Microsoft.AspNetCore.Http;
using Clinic.Application.Features.ExaminationServices.CreateService;

namespace Clinic.WebAPI.EndPoints.Admin.CreateMedicine.HttpResoponseMapper;

/// <summary>
///     Mapper for CreateMedicine feature
/// </summary>
public class CreateMedicineHttpResponseManager
{
    private readonly Dictionary<
        CreateMedicineResponseStatusCode,
        Func<CreateMedicineRequest, CreateMedicineResponse, CreateMedicineHttpResponse>
    > _dictionary;

    internal CreateMedicineHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: CreateMedicineResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: CreateMedicineResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: CreateMedicineResponseStatusCode.FORBIDEN_ACCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: CreateMedicineResponseStatusCode.MEDICINE_ALREADY_EXISTED,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        CreateMedicineRequest,
        CreateMedicineResponse,
        CreateMedicineHttpResponse
    > Resolve(CreateMedicineResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}

