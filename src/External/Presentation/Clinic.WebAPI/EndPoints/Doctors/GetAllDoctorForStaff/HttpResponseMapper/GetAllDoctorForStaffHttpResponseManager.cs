using System;
using System.Collections.Generic;
using Clinic.Application.Features.Doctors.GetAllDoctorForStaff;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Doctors.GetAllDoctorForStaff.HttpResponseMapper;

public class GetAllDoctorForStaffHttpResponseManager
{
    private readonly Dictionary<
        GetAllDoctorForStaffResponseStatusCode,
        Func<
            GetAllDoctorForStaffRequest,
            GetAllDoctorForStaffResponse,
            GetAllDoctorForStaffHttpResponse
        >
    > _dictionary;

    internal GetAllDoctorForStaffHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetAllDoctorForStaffResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody,
                }
        );

        _dictionary.Add(
            key: GetAllDoctorForStaffResponseStatusCode.ROLE_IS_NOT_STAFF,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        GetAllDoctorForStaffRequest,
        GetAllDoctorForStaffResponse,
        GetAllDoctorForStaffHttpResponse
    > Resolve(GetAllDoctorForStaffResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
