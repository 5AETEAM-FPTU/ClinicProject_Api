using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Threading.Tasks;
using System.Threading;
using Clinic.WebAPI.EndPoints.Doctors.GetAllDoctorForBooking.HttpResponseMapper;
using Microsoft.AspNetCore.Http;
using Clinic.Application.Features.Auths.Login;
using Clinic.WebAPI.EndPoints.Doctors.GetAllDoctorForBooking.Common;

namespace Clinic.WebAPI.EndPoints.Doctors.GetAllDoctorForBooking;

/// <summary>
///     GetAllDoctorForBooking endpoint.
/// </summary>
internal sealed class GetAllDoctorForBookingEndpoint
    : Endpoint<EmptyRequest, GetAllDoctorForBookingHttpResponse>
{
    public override void Configure()
    {
        Get(routePatterns: "doctor/getAllDoctorForBooking");
        AuthSchemes(authSchemeNames: JwtBearerDefaults.AuthenticationScheme);
        DontThrowIfValidationFails();
        Description(builder: builder =>
        {
            builder.ClearDefaultProduces(statusCodes: StatusCodes.Status400BadRequest);
        });
        Summary(endpointSummary: summary =>
        {
            summary.Summary = "Endpoint for Doctor feature";
            summary.Description = "This endpoint is used for display doctor for user booking.";
            summary.Response<GetAllDoctorForBookingHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = LoginResponseStatusCode.OPERATION_SUCCESS.ToAppCode()
                }
            );
        });
    }

    public override async Task<GetAllDoctorForBookingHttpResponse> ExecuteAsync(
        EmptyRequest req,
        CancellationToken ct
    )
    {
        // Get app feature response.
        var stateBag = ProcessorState<GetAllDoctorForBookingStateBag>();

        var appResponse = await stateBag.AppRequest.ExecuteAsync(ct: ct);

        // Convert to http response.
        var httpResponse = GetAllDoctorForBookingHttpResponseMapper
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
