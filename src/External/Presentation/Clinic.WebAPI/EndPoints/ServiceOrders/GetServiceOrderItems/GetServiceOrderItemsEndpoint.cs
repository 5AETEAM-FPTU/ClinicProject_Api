using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Constance;
using Clinic.Application.Features.Schedules.CreateSchedules;
using Clinic.Application.Features.ServiceOrders.GetServiceOrderItems;
using Clinic.WebAPI.EndPoints.Schedules.CreateSchedules.HttpResponseMapper;
using Clinic.WebAPI.EndPoints.ServiceOrders.GetServiceOrderItems.HttpResponseMapper;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.ServiceOrders.GetServiceOrderItems;

/// <summary>
///     GetServiceOrderItems endpoint
/// </summary>
public class GetServiceOrderItemsEndpoint
    : Endpoint<GetServiceOrderItemsRequest, GetServiceOrderItemsHttpResponse>
{
    public override void Configure()
    {
        Get("service-order/detail");
        AuthSchemes(authSchemeNames: JwtBearerDefaults.AuthenticationScheme);
        DontThrowIfValidationFails();
        Description(builder =>
        {
            builder.ClearDefaultProduces(StatusCodes.Status400BadRequest);
        });
        Summary(summary =>
        {
            summary.Summary = "Endpoint to get detail of service indication (order).";
            summary.Description = "This endpoint allows user to get detail of service indication.";
            summary.Response<GetServiceOrderItemsHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = CreateSchedulesResponseStatusCode.OPERATION_SUCCESS.ToAppCode()
                }
            );
        });
    }

    public override async Task<GetServiceOrderItemsHttpResponse> ExecuteAsync(
        GetServiceOrderItemsRequest req,
        CancellationToken ct
    )
    {
        var appResponse = await req.ExecuteAsync(ct: ct);

        var httpResponse = GetServiceOrderItemsHttpResponseMapper
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
