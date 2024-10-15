using System;
using System.Collections.Generic;
using Clinic.Application.Features.Admin.DeleteMedicineTypeById;
using Clinic.Application.Features.Admin.GetMedicineById;
using Clinic.Application.Features.ExaminationServices.GetDetailService;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Admin.GetMedicineById.HttpResponseMapper;

public class GetMedicineByIdHttpResponseManager
{
    private readonly Dictionary<
        GetMedicineByIdResponseStatusCode,
        Func<GetMedicineByIdRequest, GetMedicineByIdResponse, GetMedicineByIdHttpResponse>
    > _dictionary;

    internal GetMedicineByIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetMedicineByIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody,
                }
        );
        _dictionary.Add(
            key: GetMedicineByIdResponseStatusCode.MEDICINE_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        GetMedicineByIdRequest,
        GetMedicineByIdResponse,
        GetMedicineByIdHttpResponse
    > Resolve(GetMedicineByIdResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
