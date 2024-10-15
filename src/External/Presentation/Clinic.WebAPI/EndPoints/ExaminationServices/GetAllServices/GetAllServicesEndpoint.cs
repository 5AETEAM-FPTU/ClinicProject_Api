using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Clinic.Application.Features.Auths.Login;
using Clinic.Application.Features.ExaminationServices.GetAllServices;
using Clinic.WebAPI.EndPoints.ExaminationServices.GetAllServices.HttpResponseMapper;
using Clinic.Application.Features.ExaminationServices.GetAvailableServices;
using Clinic.WebAPI.EndPoints.ExaminationServices.GetAvailableServices.HttpResponseMapper;

namespace Clinic.WebAPI.EndPoints.ExaminationServices.GetAllServices;

/// <summary>
///     GetAllSerivces endpoint.
/// </summary>
internal sealed class GetAllServicesEndpoint
    : Endpoint<GetAllServicesRequest, GetAllServicesHttpResponse>
{
    public override void Configure()
    {
        Get(routePatterns: "services/all");
        AuthSchemes(authSchemeNames: JwtBearerDefaults.AuthenticationScheme);
        DontThrowIfValidationFails();
        Description(builder: builder =>
        {
            builder.ClearDefaultProduces(statusCodes: StatusCodes.Status400BadRequest);
        });
        Summary(endpointSummary: summary =>
        {
            summary.Summary = "Endpoint for Admin/Staff to get all services (pagination) feature";
            summary.Description = "This endpoint is used for get all serivce with pagination. Key param can be code or name of service";
                                   

            summary.Response<GetAllServicesHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = LoginResponseStatusCode.OPERATION_SUCCESS.ToAppCode()
                }
            );
        });
    }

    public override async Task<GetAllServicesHttpResponse> ExecuteAsync(
        GetAllServicesRequest req,
        CancellationToken ct
    )
    {

        var appResponse = await req.ExecuteAsync(ct: ct);

        var httpResponse = GetAllServicesHttpResponseMapper
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
