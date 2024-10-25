using System;
using System.Collections.Generic;
using Clinic.Application.Features.Admin.GetAvailableMedicines;
using Clinic.Application.Features.Admin.GetDoctorStaffProfile;
using Clinic.WebAPI.EndPoints.Admin.GetAvailableMedicines.HttpResponseMapper;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Admin.GetDoctorStaffProfile.HttpResponseMapper;

public class GetDoctorStaffProfileHttpResponseManager
{
    private readonly Dictionary<
        GetDoctorStaffProfileResponseStatusCode,
        Func<
            GetDoctorStaffProfileRequest,
            GetDoctorStaffProfileResponse,
            GetDoctorStaffProfileHttpResponse
        >
    > _dictionary;

    internal GetDoctorStaffProfileHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetDoctorStaffProfileResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody,
                }
        );

        _dictionary.Add(
            key: GetDoctorStaffProfileResponseStatusCode.USER_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: GetDoctorStaffProfileResponseStatusCode.DATABASE_OPERATION_FAILED,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
        _dictionary.Add(
            key: GetDoctorStaffProfileResponseStatusCode.ROLE_IS_NOT_PERMISSION,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        GetDoctorStaffProfileRequest,
        GetDoctorStaffProfileResponse,
        GetDoctorStaffProfileHttpResponse
    > Resolve(GetDoctorStaffProfileResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
