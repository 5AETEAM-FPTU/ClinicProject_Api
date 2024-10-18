using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Clinic.Application.Features.Auths.Login;
using Clinic.WebAPI.EndPoints.Admin.GetAllDoctor.Common;
using Clinic.WebAPI.EndPoints.Admin.GetAllDoctor.HttpResponseMapper;
using Clinic.Application.Features.Admin.GetAllDoctor;

namespace Clinic.WebAPI.EndPoints.Admin.GetAllDoctor;

/// <summary>
///     Login endpoint.
/// </summary>
internal sealed class GetAllDoctorEndpoint
    : Endpoint<GetAllDoctorRequest, GetAllDoctorHttpResponse>
{
    public override void Configure()
    {
        Get(routePatterns: "admin/doctors/all");
        AuthSchemes(authSchemeNames: JwtBearerDefaults.AuthenticationScheme);
        DontThrowIfValidationFails();
        Description(builder: builder =>
        {
            builder.ClearDefaultProduces(statusCodes: StatusCodes.Status400BadRequest);
        });
        Summary(endpointSummary: summary =>
        {
            summary.Summary = "Endpoint for Admin feature";
            summary.Description = "This endpoint is used for display all Doctors.";
            summary.Response<GetAllDoctorHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = LoginResponseStatusCode.OPERATION_SUCCESS.ToAppCode()
                }
            );
        });
    }

    public override async Task<GetAllDoctorHttpResponse> ExecuteAsync(
        GetAllDoctorRequest req,
        CancellationToken ct
    )
    {
        // Get app feature response.
        //var stateBag = ProcessorState<GetAllDoctorStateBag>();

        var appResponse = await req.ExecuteAsync(ct: ct);

        // Convert to http response.
        var httpResponse = GetAllDoctorHttpResponseMapper
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
