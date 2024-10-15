using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Features.Admin.RemoveMedicineTemporarily;
using Clinic.WebAPI.EndPoints.Admin.RemoveMedicineTemporarily.HttpResponseMapper;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Admin.RemoveMedicineTemporarily;

/// <summary>
///     RemoveMedicineTemporarily endpoint
/// </summary>
public class RemoveMedicineTemporarilyEndpoint
    : Endpoint<RemoveMedicineTemporarilyRequest, RemoveMedicineTemporarilyHttpResponse>
{
    public override void Configure()
    {
        Delete("admin/medicine/temporarily/{medicineId}");
        AuthSchemes(JwtBearerDefaults.AuthenticationScheme);
        DontThrowIfValidationFails();
        Description(builder =>
        {
            builder.ClearDefaultProduces(StatusCodes.Status400BadRequest);
        });
        Summary(summary =>
        {
            summary.Summary = "Endpoint for admin";
            summary.Description =
                "This endpoint allows admin to remove temporarily specific medicine.";
            summary.Response<RemoveMedicineTemporarilyHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode =
                        RemoveMedicineTemporarilyResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                }
            );
        });
    }

    public override async Task<RemoveMedicineTemporarilyHttpResponse> ExecuteAsync(
        RemoveMedicineTemporarilyRequest req,
        CancellationToken ct
    )
    {
        var appResponse = await req.ExecuteAsync(ct: ct);

        var httpResponse = RemoveMedicineTemporarilyHttpResponseMapper
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
