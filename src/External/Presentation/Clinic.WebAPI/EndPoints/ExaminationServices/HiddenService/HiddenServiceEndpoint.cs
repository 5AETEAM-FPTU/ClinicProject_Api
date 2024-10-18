using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Clinic.Application.Features.ExaminationServices.HiddenService;
using Clinic.WebAPI.EndPoints.ExaminationServices.HiddenService.HttpResoponseMapper;

namespace Clinic.WebAPI.EndPoints.ExaminationServices.HiddenService;

/// <summary>
///     Hidden endpoint
/// </summary>
public class HiddenServiceEndpoint : Endpoint<HiddenServiceRequest, HiddenServiceHttpResponse>
{
    public override void Configure()
    {
        Patch("services/hidden");
        AuthSchemes(JwtBearerDefaults.AuthenticationScheme);
        DontThrowIfValidationFails();
        Description(builder =>
        {
            builder.ClearDefaultProduces(StatusCodes.Status400BadRequest);
        });
        Summary(summary =>
        {
            summary.Summary = "Endpoint for admin/staff to remove service temporarity by id";
            summary.Description =
                "This endpoint allows admin/staff for remove service temporarity by id.";
            summary.Response<HiddenServiceHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = HiddenServiceResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                }
            );
        });
    }

    public override async Task<HiddenServiceHttpResponse> ExecuteAsync(
        HiddenServiceRequest req,
        CancellationToken ct
    )
    {
        var appResponse = await req.ExecuteAsync(ct: ct);

        var httpResponse = HiddenServiceHttpResponseMapper
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
