﻿using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.VNPay.Response;
using Clinic.Application.Features.OnlinePayments.CreateNewOnlinePayment;
using Clinic.Application.Features.Users.VNPays.CreatePaymentLink;
using Clinic.Application.Features.VNPays.VNPayIPN;
using Clinic.WebAPI.EndPoints.CreatePaymentLink.HttpResponseMapper;
using Clinic.WebAPI.EndPoints.OnlinePayment.HttpResponseMapper;
using Clinic.WebAPI.EndPoints.VNPayIPN.HttpResponseMapper;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.VNPayIPN;

public class VNPayIPNEndpoint
    : Endpoint<VNPayIPNRequest, VNPayIPNHttpResponse>
{
    public override void Configure()
    {
        Post("/IPN");
        AuthSchemes(JwtBearerDefaults.AuthenticationScheme);
        DontThrowIfValidationFails();
        Description(builder =>
        {
            builder.ClearDefaultProduces(StatusCodes.Status400BadRequest);
        });
        Summary(summary =>
        {
            summary.Summary = "Endpoint for creating new online payment";
            summary.Description =
                "This endpoint allow user for create new online payment after successful payment with vnpay";
            summary.Response<CreatePaymentLinkHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode =
                        VNPayIPNResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                }
            );
        });
    }

    public override async Task<VNPayIPNHttpResponse> ExecuteAsync(
        VNPayIPNRequest req,
        CancellationToken ct
    )
    {
        // Get app feature response.
        var appResponse = await req.ExecuteAsync(ct: ct);
        // Convert to http response.
        var httpResponse = VNPayIPNHttpResponseMapper
            .Get()
            .Resolve(statusCode: appResponse.StatusCode)
            .Invoke(arg1: req, arg2: appResponse);
        // Store the real http code of http response into a temporary variable.
        var httpResponseStatusCode = httpResponse.HttpCode;
        httpResponse.HttpCode = default;
        // Send http response to client.
        // The http code of http response will be stored into a temporary variable.
        await SendAsync(httpResponse, httpResponseStatusCode, ct);
        httpResponse.HttpCode = httpResponseStatusCode;
        // Set the http code of http response back to real one.
        return httpResponse;
    }
}
