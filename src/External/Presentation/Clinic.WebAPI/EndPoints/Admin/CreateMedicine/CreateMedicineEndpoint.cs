using Clinic.WebAPI.Commons.Behaviors.Validation;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Threading.Tasks;
using System.Threading;
using Clinic.Application.Features.Admin.CreateMedicine;
using Clinic.WebAPI.EndPoints.Admin.CreateMedicine.HttpResoponseMapper;
using Microsoft.AspNetCore.Http;
using Clinic.Application.Features.ExaminationServices.CreateService;

namespace Clinic.WebAPI.EndPoints.Admin.CreateMedicine;

/// <summary>
///     CreateMedicine endpoint
/// </summary>
public class CreateMedicineEndpoint : Endpoint<CreateMedicineRequest, CreateMedicineHttpResponse>
{
    public override void Configure()
    {
        Post("admin/medicine/create");
        AuthSchemes(JwtBearerDefaults.AuthenticationScheme);
        PreProcessor<ValidationPreProcessor<CreateMedicineRequest>>();
        DontThrowIfValidationFails();
        Description(builder =>
        {
            builder.ClearDefaultProduces(StatusCodes.Status400BadRequest);
        });
        Summary(summary =>
        {
            summary.Summary = "Endpoint to create medicine";
            summary.Description =
                "This endpoint allows doctor/staff for creating medicine.";
            summary.Response<CreateMedicineHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = CreateMedicineResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                }
            );
        });
    }

    public override async Task<CreateMedicineHttpResponse> ExecuteAsync(
        CreateMedicineRequest req,
        CancellationToken ct
    )
    {
        var appResponse = await req.ExecuteAsync(ct: ct);

        var httpResponse = CreateMedicineHttpResponseMapper
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
