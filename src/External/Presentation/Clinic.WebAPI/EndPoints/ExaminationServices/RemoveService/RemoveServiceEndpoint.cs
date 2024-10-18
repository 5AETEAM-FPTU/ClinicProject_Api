using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Clinic.WebAPI.EndPoints.ExaminationServices.RemoveService.HttpResoponseMapper;
using Clinic.Application.Features.ExaminationServices.RemoveService;
using Clinic.Application.Features.ExaminationServices.HiddenService;
using Clinic.WebAPI.EndPoints.ExaminationServices.HiddenService.HttpResoponseMapper;

namespace Clinic.WebAPI.EndPoints.ExaminationServices.RemoveService;

/// <summary>
///     RemoveService endpoint
/// </summary>
public class RemoveServiceEndpoint : Endpoint<RemoveServiceRequest, RemoveServiceHttpResponse>
{
    public override void Configure()
    {
        Delete("services/remove/{serviceId}");
        AuthSchemes(JwtBearerDefaults.AuthenticationScheme);
        DontThrowIfValidationFails();
        Description(builder =>
        {
            builder.ClearDefaultProduces(StatusCodes.Status400BadRequest);
        });
        Summary(summary =>
        {
            summary.Summary = "Endpoint for admin/staff to remove service permently by id";
            summary.Description =
                "This endpoint allows admin/staff for remove service permently  by id.";
            summary.Response<RemoveServiceHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = RemoveServiceResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                }
            );
        });
    }

    public override async Task<RemoveServiceHttpResponse> ExecuteAsync(
        RemoveServiceRequest req,
        CancellationToken ct
    )
    {
        var appResponse = await req.ExecuteAsync(ct: ct);

        var httpResponse = RemoveServiceHttpResponseMapper
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
