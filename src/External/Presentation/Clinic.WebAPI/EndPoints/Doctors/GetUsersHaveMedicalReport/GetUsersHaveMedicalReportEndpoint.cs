using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Threading.Tasks;
using System.Threading;
using Clinic.Application.Features.Doctors.GetUsersHaveMedicalReport;
using Clinic.WebAPI.EndPoints.Doctors.GetUsersHaveMedicalReport.HttpResponseMapper;
using Microsoft.AspNetCore.Http;
using Clinic.Application.Features.Auths.Login;

namespace Clinic.WebAPI.EndPoints.Doctors.GetUsersHaveMedicalReport;

/// <summary>
///     GetUsersHaveMedicalReport endpoint.
/// </summary>
internal sealed class GetUsersHaveMedicalReportEndpoint
    : Endpoint<GetUsersHaveMedicalReportRequest, GetUsersHaveMedicalReportHttpResponse>
{
    public override void Configure()
    {
        Get(routePatterns: "doctor/re-examination/users");
        AuthSchemes(authSchemeNames: JwtBearerDefaults.AuthenticationScheme);
        DontThrowIfValidationFails();
        Description(builder: builder =>
        {
            builder.ClearDefaultProduces(statusCodes: StatusCodes.Status400BadRequest);
        });
        Summary(endpointSummary: summary =>
        {
            summary.Summary = "Endpoint for Doctor/Staff feature";
            summary.Description =
                "This endpoint is used for display users that already have medical reports to send retreatment notification.";
            summary.Response<GetUsersHaveMedicalReportHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = LoginResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                }
            );
        });
    }

    public override async Task<GetUsersHaveMedicalReportHttpResponse> ExecuteAsync(
        GetUsersHaveMedicalReportRequest req,
        CancellationToken ct
    )
    {
        var appResponse = await req.ExecuteAsync(ct: ct);

        var httpResponse = GetUsersHaveMedicalReportHttpResponseMapper
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

