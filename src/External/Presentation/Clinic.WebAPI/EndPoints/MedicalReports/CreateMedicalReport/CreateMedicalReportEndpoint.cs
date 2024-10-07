using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Features.MedicalReports.CreateMedicalReport;
using Clinic.WebAPI.EndPoints.MedicalReports.CreateMedicalReport.HttpResponseMapper;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.OnlinePayment;

/// <summary>
///     Endpoint for CreateMedicalReport
/// </summary>
public class CreateMedicalReportEndpoint
    : Endpoint<CreateMedicalReportRequest, CreateMedicalReportHttpResponse>
{
    public override void Configure()
    {
        Post("medicalReport");
        AuthSchemes(JwtBearerDefaults.AuthenticationScheme);
        DontThrowIfValidationFails();
        Description(builder =>
        {
            builder.ClearDefaultProduces(StatusCodes.Status400BadRequest);
        });
        Summary(summary =>
        {
            summary.Summary = "Endpoint for create medical report.";
            summary.Description = "This endpoint allow user for create medical report.";
            summary.Response<CreateMedicalReportHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = CreateMedicalReportResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                }
            );
        });
    }

    public override async Task<CreateMedicalReportHttpResponse> ExecuteAsync(
        CreateMedicalReportRequest req,
        CancellationToken ct
    )
    {
        // Get app feature response.
        var appResponse = await req.ExecuteAsync(ct: ct);
        // Convert to http response.
        var httpResponse = CreateMedicalReportHttpResponseMapper
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
