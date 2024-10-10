using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Features.OnlinePayments.HandleRedirectURL;
using Clinic.WebAPI.EndPoints.Payments.HandleRedirectURL.HttpResponseMapper;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.RedirectURL;

/// <summary>
///     HandleRedirectURL endpoint.
/// </summary>
public class HandleRedirectURLEndpoint : Endpoint<HandleRedirectURLRequest>
{
    public override void Configure()
    {
        Get("payment/return-url/success");
        AllowAnonymous();
        Description(builder =>
        {
            builder.ClearDefaultProduces(StatusCodes.Status400BadRequest);
        });
        Summary(summary =>
        {
            summary.Summary = "Endpoint for redirect when payment status successfully.";
            summary.Description =
                "This endpoint allow you to redirect to the success url when payment status successfully.";
        });
    }

    public override async Task HandleAsync(HandleRedirectURLRequest request, CancellationToken ct)
    {
        var appResponse = await request.ExecuteAsync(ct: ct);
        // Convert to http response.
        var httpResponse = HandleRedirectURLHttpResponseMapper
            .Get()
            .Resolve(statusCode: appResponse.StatusCode)
            .Invoke(arg1: request, arg2: appResponse);
        // Store the real http code of http response into a temporary variable.
        var httpResponseStatusCode = httpResponse.HttpCode;

        httpResponse.HttpCode = default;

        await SendRedirectAsync(location: "https://www.facebook.com", allowRemoteRedirects: true);
    }
}
