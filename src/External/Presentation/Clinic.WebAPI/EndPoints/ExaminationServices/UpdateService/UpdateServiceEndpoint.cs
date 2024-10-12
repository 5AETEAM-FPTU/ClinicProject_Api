using Clinic.WebAPI.Commons.Behaviors.Validation;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Clinic.Application.Features.ExaminationServices.UpdateService;
using Clinic.WebAPI.EndPoints.ExaminationServices.UpdateService.HttpResoponseMapper;

namespace Clinic.WebAPI.EndPoints.ExaminationServices.UpdateService;

/// <summary>
///     CreateMedicine endpoint
/// </summary>
public class CreateMedicineEndpoint : Endpoint<UpdateServiceRequest, UpdateServiceHttpResponse>
{
    public override void Configure()
    {
        Patch("services/update/{serviceId}");
        AuthSchemes(JwtBearerDefaults.AuthenticationScheme);
        DontThrowIfValidationFails();
        Description(builder =>
        {
            builder.ClearDefaultProduces(StatusCodes.Status400BadRequest);
        });
        Summary(summary =>
        {
            summary.Summary = "Endpoint for admin/staff to update service by id";
            summary.Description =
                "This endpoint allows admin/staff for update service by id.";
            summary.Response<UpdateServiceHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = UpdateServiceResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                }
            );
        });
    }

    public override async Task<UpdateServiceHttpResponse> ExecuteAsync(
        UpdateServiceRequest req,
        CancellationToken ct
    )
    {
        var appResponse = await req.ExecuteAsync(ct: ct);

        var httpResponse = UpdateServiceHttpResponseMapper
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
