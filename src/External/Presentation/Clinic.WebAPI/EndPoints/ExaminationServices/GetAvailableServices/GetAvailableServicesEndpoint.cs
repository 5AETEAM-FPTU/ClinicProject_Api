using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Clinic.Application.Features.Auths.Login;
using Clinic.Application.Features.ExaminationServices.GetAvailableServices;
using Clinic.WebAPI.EndPoints.ExaminationServices.GetAvailableServices.HttpResponseMapper;

namespace Clinic.WebAPI.EndPoints.ExaminationServices.GetAvailableServices;

/// <summary>
///     GetAvailableSerivces endpoint.
/// </summary>
internal sealed class GetAvailableServicesEndpoint
    : Endpoint<GetAvailableServicesRequest, GetAvailableServicesHttpResponse>
{
    public override void Configure()
    {
        Get(routePatterns: "/services/available");
        AuthSchemes(authSchemeNames: JwtBearerDefaults.AuthenticationScheme);
        DontThrowIfValidationFails();
        Description(builder: builder =>
        {
            builder.ClearDefaultProduces(statusCodes: StatusCodes.Status400BadRequest);
        });
        Summary(endpointSummary: summary =>
        {
            summary.Summary = "Endpoint for Admin/Staff to get available services for ordering feature";
            summary.Description = "This endpoint is used for get all serivce for ordering. Key param can be code or name of service";


            summary.Response<GetAvailableServicesHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = LoginResponseStatusCode.OPERATION_SUCCESS.ToAppCode()
                }
            );
        });
    }

    public override async Task<GetAvailableServicesHttpResponse> ExecuteAsync(
        GetAvailableServicesRequest req,
        CancellationToken ct
    )
    {

        var appResponse = await req.ExecuteAsync(ct: ct);

        var httpResponse = GetAvailableServicesHttpResponseMapper
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
