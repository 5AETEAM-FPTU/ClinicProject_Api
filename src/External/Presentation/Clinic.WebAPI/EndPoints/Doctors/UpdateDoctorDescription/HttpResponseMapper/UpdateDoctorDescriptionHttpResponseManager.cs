using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Http;
using Clinic.Application.Features.Doctors.UpdateDoctorDescription;


namespace Clinic.WebAPI.EndPoints.Doctors.UpdateDoctorDescription.HttpResponseMapper;

public class UpdateDoctorDescriptionHttpResponseManager
{
    private readonly Dictionary<
        UpdateDoctorDescriptionByIdResponseStatusCode,
        Func<UpdateDoctorDescriptionByIdRequest, UpdateDoctorDescriptionByIdResponse, UpdateDoctorDescriptionHttpResponse>
    > _dictionary;

    internal UpdateDoctorDescriptionHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: UpdateDoctorDescriptionByIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: UpdateDoctorDescriptionByIdResponseStatusCode.USER_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: UpdateDoctorDescriptionByIdResponseStatusCode.USER_IS_TEMPORARILY_REMOVED,
            value: (request, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status417ExpectationFailed,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: UpdateDoctorDescriptionByIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status417ExpectationFailed,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

    }

    internal Func<
        UpdateDoctorDescriptionByIdRequest,
        UpdateDoctorDescriptionByIdResponse,
        UpdateDoctorDescriptionHttpResponse
    > Resolve(UpdateDoctorDescriptionByIdResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
