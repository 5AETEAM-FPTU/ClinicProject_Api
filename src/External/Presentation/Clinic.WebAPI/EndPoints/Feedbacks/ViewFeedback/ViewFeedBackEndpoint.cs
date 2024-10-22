using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Features.Feedbacks.ViewFeedback;
using Clinic.WebAPI.Commons.Behaviors.Validation;
using Clinic.WebAPI.EndPoints.Feedbacks.ViewFeedback.HttpResponseMapper;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Feedbacks.ViewFeedback;

/// <summary>
///     ViewFeedBack endpoint
/// </summary>
public class ViewFeedBackEndpoint : Endpoint<ViewFeedBackRequest, ViewFeedBackHttpResponse>
{
    public override void Configure()
    {
        Get("user/feedback/view");
        AuthSchemes(JwtBearerDefaults.AuthenticationScheme);
        PreProcessor<ValidationPreProcessor<ViewFeedBackRequest>>();
        DontThrowIfValidationFails();
        Description(builder =>
        {
            builder.ClearDefaultProduces(StatusCodes.Status400BadRequest);
        });
        Summary(summary =>
        {
            summary.Summary = "Endpoint to create medicine";
            summary.Description = "This endpoint allows user for sending feedback to doctor.";
            summary.Response<ViewFeedBackHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = ViewFeedBackResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                }
            );
        });
    }

    public override async Task<ViewFeedBackHttpResponse> ExecuteAsync(
        ViewFeedBackRequest req,
        CancellationToken ct
    )
    {
        var appResponse = await req.ExecuteAsync(ct: ct);

        var httpResponse = ViewFeedBackHttpResponseMapper
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
