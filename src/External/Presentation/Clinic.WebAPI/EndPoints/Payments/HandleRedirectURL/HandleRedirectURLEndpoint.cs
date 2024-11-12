using System.Globalization;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Features.OnlinePayments.HandleRedirectURL;
using Clinic.Configuration.Presentation.Authentication;
using Clinic.WebAPI.EndPoints.Payments.HandleRedirectURL.HttpResponseMapper;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.RedirectURL;

/// <summary>
///     HandleRedirectURL endpoint.
/// </summary>
public class HandleRedirectURLEndpoint : Endpoint<HandleRedirectURLRequest>
{
    private BaseEndpointUrlOption _baseEndpointUrlOption { get; set; }

    public HandleRedirectURLEndpoint(BaseEndpointUrlOption baseEndpointUrlOption)
    {
        _baseEndpointUrlOption = baseEndpointUrlOption;
    }

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

        var httpResponse = HandleRedirectURLHttpResponseMapper
            .Get()
            .Resolve(statusCode: appResponse.StatusCode)
            .Invoke(arg1: request, arg2: appResponse);

        var httpResponseStatusCode = httpResponse.HttpCode;

        string redirectUrl;
        if (httpResponseStatusCode == StatusCodes.Status202Accepted)
        {
            redirectUrl = $"{_baseEndpointUrlOption.Client}/vi/user/treatment-calendar/booking";
        }
        else
        {
            redirectUrl =
                $"{_baseEndpointUrlOption.Client}/vi/user/treatment-calendar/booking/payment?code={httpResponseStatusCode}";

            if (httpResponseStatusCode == 200)
            {
                var properties = httpResponse.Body.GetType().GetProperties();

                var queryParams = new StringBuilder();
                foreach (var property in properties)
                {
                    var propertyName = property.Name;
                    var propertyValue = property.GetValue(httpResponse.Body, null)?.ToString();

                    if (!string.IsNullOrEmpty(propertyValue))
                    {
                        queryParams.Append(
                            $"{WebUtility.UrlEncode(propertyName)}={WebUtility.UrlEncode(propertyValue)}&"
                        );
                    }
                }

                queryParams.Remove(queryParams.Length - 1, 1);

                redirectUrl = $"{redirectUrl}&{queryParams}";
            }
        }

        await SendRedirectAsync(location: redirectUrl, allowRemoteRedirects: true);
    }
}
