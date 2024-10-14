using System.Collections.Generic;
using System;
using Clinic.Application.Features.Admin.CreateNewMedicineGroup;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Admin.CreateNewMedicineGroup.HttpResponseMapper;

/// <summary>
///     Mapper for CreateNewMedicineGroup feature
/// </summary>
public class CreateNewMedicineGroupHttpResponseManager
{
    private readonly Dictionary<
        CreateNewMedicineGroupResponseStatusCode,
        Func<CreateNewMedicineGroupRequest, CreateNewMedicineGroupResponse, CreateNewMedicineGroupHttpResponse>
    > _dictionary;

    internal CreateNewMedicineGroupHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: CreateNewMedicineGroupResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: CreateNewMedicineGroupResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: CreateNewMedicineGroupResponseStatusCode.FORBIDEN_ACCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: CreateNewMedicineGroupResponseStatusCode.MEDICINE_GROUP_ALREADY_EXISTED,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        CreateNewMedicineGroupRequest,
        CreateNewMedicineGroupResponse,
        CreateNewMedicineGroupHttpResponse
    > Resolve(CreateNewMedicineGroupResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
