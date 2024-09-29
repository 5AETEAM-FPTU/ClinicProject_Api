﻿using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Features.Doctors.UpdatePrivateDoctorInfo;
using Clinic.Application.Features.Schedules.GetSchedulesByDate;
using Clinic.WebAPI.Commons.Behaviors.Validation;
using Clinic.WebAPI.EndPoints.Schedules.GetSchedulesByDate.HttpResponseMapper;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Doctors.GetSchedulesByDate;

/// <summary>
///     GetSchedulesByDate endpoint
/// </summary>
public class GetSchedulesByDateEndpoint
    : Endpoint<GetSchedulesByDateRequest, GetSchedulesByDateHttpResponse>
{
    public override void Configure()
    {
        Get("schedules/{date}");
        PreProcessor<ValidationPreProcessor<GetSchedulesByDateRequest>>();
        AllowAnonymous();
        DontThrowIfValidationFails();
        Description(builder =>
        {
            builder.ClearDefaultProduces(StatusCodes.Status400BadRequest);
        });
        Summary(summary =>
        {
            summary.Summary = "Endpoint to get schedules by date.";
            summary.Description = "This endpoint allows to get schedules by date.";
            summary.Response<GetSchedulesByDateHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = GetSchedulesByDateResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                }
            );
        });
    }

    public override async Task<GetSchedulesByDateHttpResponse> ExecuteAsync(
        GetSchedulesByDateRequest req,
        CancellationToken ct
    )
    {
        var appResponse = await req.ExecuteAsync(ct: ct);

        var httpResponse = GetSchedulesByDateHttpResponseMapper
            .Get()
            .Resolve(statusCode: appResponse.StatusCode)
            .Invoke(arg1: req, arg2: appResponse);

        var httpResponseStatusCode = httpResponse.HttpCode;
        httpResponse.HttpCode = default;

        await SendAsync(httpResponse, httpResponseStatusCode, ct);

        httpResponse.HttpCode = httpResponseStatusCode;

        return httpResponse;
    }
}