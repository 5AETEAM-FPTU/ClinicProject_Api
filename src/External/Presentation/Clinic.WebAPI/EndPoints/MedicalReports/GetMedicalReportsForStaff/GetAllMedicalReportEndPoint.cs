using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Features.Auths.Login;
using Clinic.Application.Features.Enums.GetAllAppointmentStatus;
using Clinic.Application.Features.MedicalReports.GetMedicalReportsForStaff;
using Clinic.WebAPI.EndPoints.MedicalReports.GetMedicalReportsForStaff.HttpResponseMapper;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.MedicalReports.GetMedicalReportsForStaff;

/// <summary>
///     GetMedicalReportsForStaff endpoint.
/// </summary>
internal sealed class GetMedicalReportsForStaffEndpoint
    : Endpoint<GetMedicalReportsForStaffRequest, GetMedicalReportsForStaffHttpResponse>
{
    public override void Configure()
    {
        Get(routePatterns: "medical-report/staff/all");
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
            summary.Response<GetMedicalReportsForStaffHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = LoginResponseStatusCode.OPERATION_SUCCESS.ToAppCode()
                }
            );
        });
    }

    public override async Task<GetMedicalReportsForStaffHttpResponse> ExecuteAsync(
        GetMedicalReportsForStaffRequest req,
        CancellationToken ct
    )
    {
        // Get app feature response.

        var appResponse = await req.ExecuteAsync(ct: ct);

        // Convert to http response.
        var httpResponse = GetMedicalReportsForStaffHttpResponseMapper
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
