using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Features.Feedbacks.DoctorGetAllFeedbacks;
using Clinic.WebAPI.Commons.Behaviors.Validation;
using Clinic.WebAPI.EndPoints.Feedbacks.DoctorGetAllFeedbacks.HttpResponseMapper;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Feedbacks.DoctorGetAllFeedbacks;

/// <summary>
///     DoctorGetAllFeedback endpoint
/// </summary>
public class DoctorGetAllFeedBackEndpoint : Endpoint<DoctorGetAllFeedBackRequest, DoctorGetAllFeedBackHttpResponse>
{
    public override void Configure()
    {
        Get("doctor/feedbacks/all");
        AuthSchemes(JwtBearerDefaults.AuthenticationScheme);
        PreProcessor<ValidationPreProcessor<DoctorGetAllFeedBackRequest>>();
        DontThrowIfValidationFails();
        Description(builder =>
        {
            builder.ClearDefaultProduces(StatusCodes.Status400BadRequest);
        });
        Summary(summary =>
        {
            summary.Summary = "Endpoint to create medicine";
            summary.Description = "This endpoint allows user for sending feedback to doctor.";
            summary.Response<DoctorGetAllFeedBackHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = DoctorGetAllFeedBackResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                }
            );
        });
    }

    public override async Task<DoctorGetAllFeedBackHttpResponse> ExecuteAsync(
        DoctorGetAllFeedBackRequest req,
        CancellationToken ct
    )
    {
        var appResponse = await req.ExecuteAsync(ct: ct);

        var httpResponse = DoctorGetAllFeedBackHttpResponseMapper
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
