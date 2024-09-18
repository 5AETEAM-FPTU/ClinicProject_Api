using Clinic.Application.Features.Users.GetProfileDoctor;
using System.Collections.Generic;
using System;
using Clinic.Application.Features.Enums.GetAllDoctorStaffType;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Enums.GetAllDoctorStaffType.HttpResponseMapper;

public class GetAllDoctorStaffTypeHttpResponseManager
{
    private readonly Dictionary<
        GetAllDoctorStaffTypeResponseStatusCode,
        Func<GetAllDoctorStaffTypeRequest, GetAllDoctorStaffTypeResponse, GetAllDoctorStaffTypeHttpResponse>
    > _dictionary;

    internal GetAllDoctorStaffTypeHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetAllDoctorStaffTypeResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );

        _dictionary.Add(
            key: GetAllDoctorStaffTypeResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
               new()
               {
                   HttpCode = StatusCodes.Status500InternalServerError,
                   AppCode = response.StatusCode.ToAppCode(),
               }
        );

    }

    internal Func<GetAllDoctorStaffTypeRequest, GetAllDoctorStaffTypeResponse, GetAllDoctorStaffTypeHttpResponse> Resolve(
        GetAllDoctorStaffTypeResponseStatusCode statusCode
    )
    {
        return _dictionary[statusCode];
    }
}
