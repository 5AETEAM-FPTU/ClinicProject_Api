using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Features.Users.GetAllMedicalReports;
using Clinic.WebAPI.EndPoints.Users.GetAllMedicalReports.HttpResponseMapper;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Users.GetAllMedicalReports;

/// <summary>
///     GetAllMedicalReport endpoint.
/// </summary>
internal sealed class GetAllUserMedicalReportsEndPoint
    : Endpoint<GetAllUserMedicalReportsRequest, GetAllUserMedicalReportsHttpResponse>
{
    public override void Configure()
    {
        Get(routePatterns: "/user/medical-report/all");
        AuthSchemes(authSchemeNames: JwtBearerDefaults.AuthenticationScheme);
        DontThrowIfValidationFails();
        Description(builder: builder =>
        {
            builder.ClearDefaultProduces(statusCodes: StatusCodes.Status400BadRequest);
        });
        Summary(endpointSummary: summary =>
        {
            summary.Summary = "Endpoint for User to get all medical reports feature (pagination)";
            summary.Description = "This endpoint is used for user to get all medical reports feature (pagination). Keywork: search by name of doctor";
            summary.Response<GetAllUserMedicalReportsHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = GetAllUserMedicalReportsResponseStatusCode.OPERATION_SUCCESS.ToAppCode()
                }
            );
        });
    }

    public override async Task<GetAllUserMedicalReportsHttpResponse> ExecuteAsync(
        GetAllUserMedicalReportsRequest req,
        CancellationToken ct
    )
    {
        // Get app feature response.

        var appResponse = await req.ExecuteAsync(ct: ct);

        // Convert to http response.
        var httpResponse = GetAllUserMedicalReportsHttpResponseMapper
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
