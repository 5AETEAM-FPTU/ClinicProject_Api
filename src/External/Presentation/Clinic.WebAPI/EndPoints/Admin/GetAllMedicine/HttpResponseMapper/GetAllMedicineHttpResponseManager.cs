using System.Collections.Generic;
using System;
using Clinic.Application.Features.Admin.GetAllMedicine;
using Microsoft.AspNetCore.Http;
using Clinic.Application.Features.ExaminationServices.GetAllServices;

namespace Clinic.WebAPI.EndPoints.Admin.GetAllMedicine.HttpResponseMapper;

public class GetAllMedicineHttpResponseManager
{
    private readonly Dictionary<
        GetAllMedicineResponseStatusCode,
        Func<GetAllMedicineRequest, GetAllMedicineResponse, GetAllMedicineHttpResponse>
    > _dictionary;

    internal GetAllMedicineHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetAllMedicineResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );



        _dictionary.Add(
            key: GetAllMedicineResponseStatusCode.ROLE_IS_NOT_ADMIN,
            value: (request, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        GetAllMedicineRequest,
        GetAllMedicineResponse,
        GetAllMedicineHttpResponse
    > Resolve(GetAllMedicineResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}

