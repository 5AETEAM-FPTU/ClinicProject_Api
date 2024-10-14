using Clinic.WebAPI.Commons.Behaviors.Validation;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Threading.Tasks;
using System.Threading;
using Clinic.Application.Features.Admin.CreateNewMedicineType;
using Clinic.WebAPI.EndPoints.Admin.CreateNewMedicineType.HttpResponseMapper;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Admin.CreateNewMedicineType;

/// <summary>
///     CreateNewMedicineType endpoint
/// </summary>
public class CreateNewMedicineTypeEndpoint : Endpoint<CreateNewMedicineTypeRequest, CreateNewMedicineTypeHttpResponse>
{
    public override void Configure()
    {
        Post("admin/medicineType/create");
        AuthSchemes(JwtBearerDefaults.AuthenticationScheme);
        PreProcessor<ValidationPreProcessor<CreateNewMedicineTypeRequest>>();
        DontThrowIfValidationFails();
        Description(builder =>
        {
            builder.ClearDefaultProduces(StatusCodes.Status400BadRequest);
        });
        Summary(summary =>
        {
            summary.Summary = "Endpoint to create medicine";
            summary.Description =
                "This endpoint allows doctor/staff for creating medicine type.";
            summary.Response<CreateNewMedicineTypeHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = CreateNewMedicineTypeResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                }
            );
        });
    }

    public override async Task<CreateNewMedicineTypeHttpResponse> ExecuteAsync(
        CreateNewMedicineTypeRequest req,
        CancellationToken ct
    )
    {
        var appResponse = await req.ExecuteAsync(ct: ct);

        var httpResponse = CreateNewMedicineTypeHttpResponseMapper
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
