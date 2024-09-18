﻿using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Http;
using Clinic.Application.Features.Users.UpdatePrivateDoctorInfoById;
using Clinic.Application.Features.Users.UpdateDoctorDescription;
using Clinic.Application.Commons.Abstractions.UpdateDoctorDescription;
using Clinic.Application.Features.Users.UpdateDoctorAchievement;


namespace Clinic.WebAPI.EndPoints.Users.UpdateDoctorAchievement.HttpResponseMapper;

public class UpdateDoctorAchievementHttpResponseManager
{
    private readonly Dictionary<
        UpdateDoctorAchievementByIdResponseStatusCode,
        Func<UpdateDoctorAchievementByIdRequest, UpdateDoctorAchievementByIdResponse, UpdateDoctorAchievementHttpResponse>
    > _dictionary;

    internal UpdateDoctorAchievementHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: UpdateDoctorAchievementByIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: UpdateDoctorAchievementByIdResponseStatusCode.USER_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: UpdateDoctorAchievementByIdResponseStatusCode.USER_IS_TEMPORARILY_REMOVED,
            value: (request, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status417ExpectationFailed,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: UpdateDoctorAchievementByIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status417ExpectationFailed,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

    }

    internal Func<
        UpdateDoctorAchievementByIdRequest,
        UpdateDoctorAchievementByIdResponse,
        UpdateDoctorAchievementHttpResponse
    > Resolve(UpdateDoctorAchievementByIdResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
