using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Features.Auths.Login;
using Clinic.Application.Features.Doctors.GetAllMedicalReport;
using Clinic.Application.Features.Enums.GetAllAppointmentStatus;
using Clinic.WebAPI.EndPoints.Doctors.GetAllMedicalReport.HttpResponseMapper;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Doctors.GetAllMedicalReport;

/// <summary>
///     GetAllMedicalReport endpoint.
/// </summary>
internal sealed class GetAllMedicalReportEndpoint
    : Endpoint<GetAllMedicalReportRequest, GetAllMedicalReportHttpResponse>
{
    public override void Configure()
    {
        Get(routePatterns: "medical-report/all");
        AuthSchemes(authSchemeNames: JwtBearerDefaults.AuthenticationScheme);
        DontThrowIfValidationFails();
        Description(builder: builder =>
        {
            builder.ClearDefaultProduces(statusCodes: StatusCodes.Status400BadRequest);
        });
        Summary(endpointSummary: summary =>
        {
            summary.Summary = "Endpoint for Doctor feature";
            summary.Description = "This endpoint is used for display all medical report.";
            summary.Response<GetAllMedicalReportHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = LoginResponseStatusCode.OPERATION_SUCCESS.ToAppCode()
                }
            );
        });
    }

    public override async Task<GetAllMedicalReportHttpResponse> ExecuteAsync(
        GetAllMedicalReportRequest req,
        CancellationToken ct
    )
    {
        // Get app feature response.

        var appResponse = await req.ExecuteAsync(ct: ct);

        // Convert to http response.
        var httpResponse = GetAllMedicalReportHttpResponseMapper
            .Get()
            .Resolve(statusCode: appResponse.StatusCode)
            .Invoke(arg1: req, arg2: appResponse);

        /*
        * Store the real http code of http response into a temporary variable.
        * Set the http code of http response to default for not serializing.
        */
        var httpResponseStatusCode = httpResponse.HttpCode;
        httpResponse.HttpCode = default;

        // Send http response to client.
        await SendAsync(
            response: httpResponse,
            statusCode: httpResponseStatusCode,
            cancellation: ct
        );

        // Set the http code of http response back to real one.
        httpResponse.HttpCode = httpResponseStatusCode;

        return httpResponse;
    }
}
