using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Features.Appointments.GetAbsentAppointment;
using Clinic.Application.Features.Appointments.GetAbsentForStaff;
using Clinic.Application.Features.Auths.Login;
using Clinic.WebAPI.EndPoints.Appointments.GetAbsentForStaff.HttpResponseMapper;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Appointments.GetAbsentForStaff;

/// <summary>
///     GetAbsentForStaff endpoint.
/// </summary>
internal sealed class GetAbsentForStaffEndpoint
    : Endpoint<GetAbsentForStaffRequest, GetAbsentForStaffHttpResponse>
{
    public override void Configure()
    {
        Get(routePatterns: "appointment/staff/absent");
        AuthSchemes(authSchemeNames: JwtBearerDefaults.AuthenticationScheme);
        DontThrowIfValidationFails();
        Description(builder: builder =>
        {
            builder.ClearDefaultProduces(statusCodes: StatusCodes.Status400BadRequest);
        });
        Summary(endpointSummary: summary =>
        {
            summary.Summary = "Endpoint for display absent appointment information";
            summary.Description =
                "This endpoint is used for display absent appointment information.";
            summary.Response<GetAbsentForStaffHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = LoginResponseStatusCode.OPERATION_SUCCESS.ToAppCode()
                }
            );
        });
    }

    public override async Task<GetAbsentForStaffHttpResponse> ExecuteAsync(
        GetAbsentForStaffRequest req,
        CancellationToken ct
    )
    {
        var appResponse = await req.ExecuteAsync(ct: ct);

        // Convert to http response.
        var httpResponse = GetAbsentForStaffHttpResponseMapper
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
