using Clinic.WebAPI.Commons.Behaviors.Validation;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Threading.Tasks;
using System.Threading;
using Clinic.Application.Features.Admin.CreateNewMedicineGroup;
using Clinic.WebAPI.EndPoints.Admin.CreateNewMedicineGroup.HttpResponseMapper;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Admin.CreateNewMedicineGroup;

/// <summary>
///     CreateNewMedicineGroup endpoint
/// </summary>
public class CreateNewMedicineGroupEndpoint : Endpoint<CreateNewMedicineGroupRequest, CreateNewMedicineGroupHttpResponse>
{
    public override void Configure()
    {
        Post("admin/medicineGroup/create");
        AuthSchemes(JwtBearerDefaults.AuthenticationScheme);
        PreProcessor<ValidationPreProcessor<CreateNewMedicineGroupRequest>>();
        DontThrowIfValidationFails();
        Description(builder =>
        {
            builder.ClearDefaultProduces(StatusCodes.Status400BadRequest);
        });
        Summary(summary =>
        {
            summary.Summary = "Endpoint to create medicine";
            summary.Description =
                "This endpoint allows doctor/staff for creating medicine group.";
            summary.Response<CreateNewMedicineGroupHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = CreateNewMedicineGroupResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                }
            );
        });
    }

    public override async Task<CreateNewMedicineGroupHttpResponse> ExecuteAsync(
        CreateNewMedicineGroupRequest req,
        CancellationToken ct
    )
    {
        var appResponse = await req.ExecuteAsync(ct: ct);

        var httpResponse = CreateNewMedicineGroupHttpResponseMapper
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
