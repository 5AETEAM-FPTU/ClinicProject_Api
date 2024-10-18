using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Features.Admin.DeleteMedicineGroupById;
using Clinic.WebAPI.EndPoints.Admin.DeleteMedicineGroupById.HttpResponseMapper;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Admin.DeleteMedicineGroupById;

/// <summary>
///     DeleteMedicineGroupById endpoint
/// </summary>
public class DeleteMedicineGroupByIdEndpoint
    : Endpoint<DeleteMedicineGroupByIdRequest, DeleteMedicineGroupByIdHttpResponse>
{
    public override void Configure()
    {
        Delete("admin/medicineGroup/remove/{medicineGroupId}");
        AuthSchemes(JwtBearerDefaults.AuthenticationScheme);
        DontThrowIfValidationFails();
        Description(builder =>
        {
            builder.ClearDefaultProduces(StatusCodes.Status400BadRequest);
        });
        Summary(summary =>
        {
            summary.Summary = "Endpoint for admin";
            summary.Description = "This endpoint allows admin to remove specific medicine group.";
            summary.Response<DeleteMedicineGroupByIdHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode =
                        DeleteMedicineGroupByIdResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                }
            );
        });
    }

    public override async Task<DeleteMedicineGroupByIdHttpResponse> ExecuteAsync(
        DeleteMedicineGroupByIdRequest req,
        CancellationToken ct
    )
    {
        var appResponse = await req.ExecuteAsync(ct: ct);

        var httpResponse = DeleteMedicineGroupByIdHttpResponseMapper
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
