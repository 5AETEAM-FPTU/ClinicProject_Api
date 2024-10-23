using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Features.Admin.RemovedDoctorTemporarily;
using Clinic.WebAPI.EndPoints.Admin.RemovedDoctorTemporarily.HttpResponseMapper;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Admin.RemovedDoctorTemporarily;

/// <summary>
///     RemovedDoctorTemporarily endpoint
/// </summary>
public class RemovedDoctorTemporarilyEndpoint
    : Endpoint<RemovedDoctorTemporarilyRequest, RemovedDoctorTemporarilyHttpResponse>
{
    public override void Configure()
    {
        Delete("admin/doctor/{doctorId}");
        AuthSchemes(JwtBearerDefaults.AuthenticationScheme);
        DontThrowIfValidationFails();
        Description(builder =>
        {
            builder.ClearDefaultProduces(StatusCodes.Status400BadRequest);
        });
        Summary(summary =>
        {
            summary.Summary = "Endpoint for admin";
            summary.Description = "This endpoint allows admin to remove temporarily a doctor.";
            summary.Response<RemovedDoctorTemporarilyHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode =
                        RemovedDoctorTemporarilyResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                }
            );
        });
    }

    public override async Task<RemovedDoctorTemporarilyHttpResponse> ExecuteAsync(
        RemovedDoctorTemporarilyRequest req,
        CancellationToken ct
    )
    {
        var appResponse = await req.ExecuteAsync(ct: ct);

        var httpResponse = RemovedDoctorTemporarilyHttpResponseMapper
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
