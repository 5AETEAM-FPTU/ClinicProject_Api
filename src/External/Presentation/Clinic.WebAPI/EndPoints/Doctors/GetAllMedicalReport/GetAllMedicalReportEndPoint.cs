using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Threading.Tasks;
using System.Threading;
using Clinic.WebAPI.EndPoints.Doctors.GetAllMedicalReport.HttpResponseMapper;
using Microsoft.AspNetCore.Http;
using Clinic.Application.Features.Auths.Login;
using Clinic.WebAPI.EndPoints.Doctors.GetAllMedicalReport.Common;

namespace Clinic.WebAPI.EndPoints.Doctors.GetAllMedicalReport;

/// <summary>
///     GetAllMedicalReport endpoint.
/// </summary>
internal sealed class GetAllMedicalReportEndpoint
    : Endpoint<EmptyRequest, GetAllMedicalReportHttpResponse>
{
    public override void Configure()
    {
        Get(routePatterns: "doctor/getAllMedicalReport");
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
        EmptyRequest req,
        CancellationToken ct
    )
    {
        // Get app feature response.
        var stateBag = ProcessorState<GetAllMedicalReportStateBag>();

        var appResponse = await stateBag.AppRequest.ExecuteAsync(ct: ct);

        // Convert to http response.
        var httpResponse = GetAllMedicalReportHttpResponseMapper
            .Get()
            .Resolve(statusCode: appResponse.StatusCode)
            .Invoke(arg1: stateBag.AppRequest, arg2: appResponse);

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