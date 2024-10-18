using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Clinic.Application.Features.Auths.Login;
using Clinic.WebAPI.EndPoints.Admin.GetAllUser.Common;
using Clinic.WebAPI.EndPoints.Admin.GetAllUser.HttpResponseMapper;
using Clinic.Application.Features.Admin.GetAllUser;

namespace Clinic.WebAPI.EndPoints.Admin.GetAllUser;

/// <summary>
///     Login endpoint.
/// </summary>
internal sealed class GetAllUserEndPoint
    : Endpoint<GetAllUserRequest, GetAllUserHttpResponse>
{
    public override void Configure()
    {
        Get(routePatterns: "admin/users/all");
        AuthSchemes(authSchemeNames: JwtBearerDefaults.AuthenticationScheme);
        DontThrowIfValidationFails();
        Description(builder: builder =>
        {
            builder.ClearDefaultProduces(statusCodes: StatusCodes.Status400BadRequest);
        });
        Summary(endpointSummary: summary =>
        {
            summary.Summary = "Endpoint for Admin feature";
            summary.Description = "This endpoint is used for display all Patients.";
            summary.Response<GetAllUserHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = LoginResponseStatusCode.OPERATION_SUCCESS.ToAppCode()
                }
            );
        });
    }

    public override async Task<GetAllUserHttpResponse> ExecuteAsync(
        GetAllUserRequest req,
        CancellationToken ct
    )
    {
        // Get app feature response.
        //var stateBag = ProcessorState<GetAllUserStateBag>();

        var appResponse = await req.ExecuteAsync(ct: ct);

        // Convert to http response.
        var httpResponse = GetAllUserHttpResponseMapper
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
