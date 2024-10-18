using System.Net;
using System.Text;
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

        var httpResponse = HandleRedirectURLHttpResponseMapper
            .Get()
            .Resolve(statusCode: appResponse.StatusCode)
            .Invoke(arg1: request, arg2: appResponse);

        var httpResponseStatusCode = httpResponse.HttpCode;

        var redirectUrl =
            $"http://localhost:3000/vi/user/treatment-calendar/booking/payment?code={httpResponseStatusCode}";

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

        await SendRedirectAsync(location: redirectUrl, allowRemoteRedirects: true);
    }
}
