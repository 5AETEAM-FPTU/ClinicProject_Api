using Clinic.WebAPI.Commons.Behaviors.Validation;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Clinic.Application.Features.ExaminationServices.CreateService;
using Clinic.WebAPI.EndPoints.ExaminationServices.CreateService.HttpResoponseMapper;

namespace Clinic.WebAPI.EndPoints.ExaminationServices.CreateService;

/// <summary>
///     CreateMedicine endpoint
/// </summary>
public class CreateMedicineEndpoint : Endpoint<CreateServiceRequest, CreateServiceHttpResponse>
{
    public override void Configure()
    {
        Post("services/create");
        AuthSchemes(JwtBearerDefaults.AuthenticationScheme);
        PreProcessor<ValidationPreProcessor<CreateServiceRequest>>();
        DontThrowIfValidationFails();
        Description(builder =>
        {
            builder.ClearDefaultProduces(StatusCodes.Status400BadRequest);
        });
        Summary(summary =>
        {
            summary.Summary = "Endpoint for admin/staff to add service";
            summary.Description =
                "This endpoint allows admin/staff for adding service.";
            summary.Response<CreateServiceHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = CreateServiceResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                }
            );
        });
    }

    public override async Task<CreateServiceHttpResponse> ExecuteAsync(
        CreateServiceRequest req,
        CancellationToken ct
    )
    {
        var appResponse = await req.ExecuteAsync(ct: ct);

        var httpResponse = CreateServiceHttpResponseMapper
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
