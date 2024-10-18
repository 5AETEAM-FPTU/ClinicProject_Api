using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Threading.Tasks;
using System.Threading;
using Clinic.Application.Features.Doctors.GetUserInforById;
using Clinic.WebAPI.EndPoints.Doctors.GetUserInforById.HttpResponseMapper;
using Microsoft.AspNetCore.Http;
using Clinic.Application.Features.Auths.Login;

namespace Clinic.WebAPI.EndPoints.Doctors.GetUserInforById;

/// <summary>
///     GetUserInforById endpoint.
/// </summary>
internal sealed class GetUserInforByIdEndpoint
    : Endpoint<GetUserInforByIdRequest, GetUserInforByIdHttpResponse>
{
    public override void Configure()
    {
        Get(routePatterns: "doctor/user/detail/{UserId}");
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
                "This endpoint is used for display user information in details.";
            summary.Response<GetUserInforByIdHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = LoginResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                }
            );
        });
    }

    public override async Task<GetUserInforByIdHttpResponse> ExecuteAsync(
        GetUserInforByIdRequest req,
        CancellationToken ct
    )
    {
        var appResponse = await req.ExecuteAsync(ct: ct);

        var httpResponse = GetUserInforByIdHttpResponseMapper
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
