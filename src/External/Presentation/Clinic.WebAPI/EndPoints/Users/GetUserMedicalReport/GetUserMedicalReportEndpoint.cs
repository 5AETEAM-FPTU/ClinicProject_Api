using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Features.Auths.Login;
using Clinic.Application.Features.Users.GetUserMedicalReport;
using Clinic.WebAPI.EndPoints.Users.GetUserMedicalReport.HttpResponseMapper;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Users.GetUserMedicalReport;

/// <summary>
///     GetUserMedicalReport endpoint.
/// </summary>
internal sealed class GetUserMedicalReportEndpoint
    : Endpoint<GetUserMedicalReportRequest, GetUserMedicalReportHttpResponse>
{
    public override void Configure()
    {
        Get(routePatterns: "user/medical-report/detail");
        AuthSchemes(authSchemeNames: JwtBearerDefaults.AuthenticationScheme);
        DontThrowIfValidationFails();
        Description(builder: builder =>
        {
            builder.ClearDefaultProduces(statusCodes: StatusCodes.Status400BadRequest);
        });
        Summary(endpointSummary: summary =>
        {
            summary.Summary = "Endpoint for User feature";
            summary.Description =
                "This endpoint is used for display user medical report in details.";
            summary.Response<GetUserMedicalReportHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = LoginResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                }
            );
        });
    }

    public override async Task<GetUserMedicalReportHttpResponse> ExecuteAsync(
        GetUserMedicalReportRequest req,
        CancellationToken ct
    )
    {
        var appResponse = await req.ExecuteAsync(ct: ct);

        var httpResponse = GetUserMedicalReportHttpResponseMapper
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
