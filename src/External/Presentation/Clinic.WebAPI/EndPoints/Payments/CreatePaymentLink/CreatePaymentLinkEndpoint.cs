using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Features.OnlinePayments.CreateNewOnlinePayment;
using Clinic.Application.Features.OnlinePayments.CreatePaymentLink;
using Clinic.WebAPI.EndPoints.Payments.CreatePaymentLink.HttpResponseMapper;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Payments.CreatePaymentLink;

/// <summary>
///     Endpoint for CreatePaymentLinkEndpoint.
/// </summary>
public class CreatePaymentLinkEndpoint
    : Endpoint<CreatePaymentLinkRequest, CreatePaymentLinkHttpResponse>
{
    public override void Configure()
    {
        Post("payment/generate-link");
        AuthSchemes(JwtBearerDefaults.AuthenticationScheme);
        DontThrowIfValidationFails();
        Description(builder =>
        {
            builder.ClearDefaultProduces(StatusCodes.Status400BadRequest);
        });
        Summary(summary =>
        {
            summary.Summary = "Endpoint for generating link payment";
            summary.Description =
                "This endpoint allow user for generate link payment to redirect to gateway payment.";
            summary.Response<CreatePaymentLinkHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode =
                        CreateNewOnlinePaymentResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                }
            );
        });
    }

    public override async Task<CreatePaymentLinkHttpResponse> ExecuteAsync(
        CreatePaymentLinkRequest req,
        CancellationToken ct
    )
    {
        // Get app feature response.
        var appResponse = await req.ExecuteAsync(ct: ct);
        // Convert to http response.
        var httpResponse = CreatePaymentLinkHttpResponseMapper
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
