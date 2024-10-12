using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions.GetProfileUser;
using Clinic.Application.Features.Auths.Login;
using Clinic.Application.Features.ExaminationServices.GetDetailService;
using Clinic.WebAPI.EndPoints.ExaminationServices.GetDetailService.HttpResponseMapper;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.ExaminationServices.GetDetailService;

/// <summary>
///     Login endpoint.
/// </summary>
internal sealed class GetDetailServiceEndpoint : Endpoint<GetDetailServiceRequest, GetDetailServiceHttpResponse>
{
    public override void Configure()
    {
        Get(routePatterns: "services/detail/{serviceId}");
        AuthSchemes(authSchemeNames: JwtBearerDefaults.AuthenticationScheme);
        DontThrowIfValidationFails();
        Description(builder: builder =>
        {
            builder.ClearDefaultProduces(statusCodes: StatusCodes.Status400BadRequest);
        });
        Summary(endpointSummary: summary =>
        {
            summary.Summary = "Endpoint for User feature";
            summary.Description = "This endpoint is used for display profile user.";
            summary.Response<GetDetailServiceHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = LoginResponseStatusCode.OPERATION_SUCCESS.ToAppCode()
                }
            );
        });
    }

    public override async Task<GetDetailServiceHttpResponse> ExecuteAsync(
        GetDetailServiceRequest req,
        CancellationToken ct
    )
    {
        var appResponse = await req.ExecuteAsync(ct: ct);

        var httpResponse = GetDetailServiceHttpResponseMapper
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
