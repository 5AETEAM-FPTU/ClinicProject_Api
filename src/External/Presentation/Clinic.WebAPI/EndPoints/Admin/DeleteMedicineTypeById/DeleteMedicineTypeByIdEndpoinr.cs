using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Features.Admin.DeleteMedicineTypeById;
using Clinic.WebAPI.EndPoints.Admin.DeleteMedicineTypeById.HttpResponseMapper;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Admin.DeleteMedicineTypeById;

/// <summary>
///     DeleteMedicineTypeById endpoint
/// </summary>
public class DeleteMedicineTypeByIdEndpoint
    : Endpoint<DeleteMedicineTypeByIdRequest, DeleteMedicineTypeByIdHttpResponse>
{
    public override void Configure()
    {
        Delete("admin/medicineType/remove/{medicineTypeId}");
        AuthSchemes(JwtBearerDefaults.AuthenticationScheme);
        DontThrowIfValidationFails();
        Description(builder =>
        {
            builder.ClearDefaultProduces(StatusCodes.Status400BadRequest);
        });
        Summary(summary =>
        {
            summary.Summary = "Endpoint for admin";
            summary.Description = "This endpoint allows admin to remove specific medicine type.";
            summary.Response<DeleteMedicineTypeByIdHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode =
                        DeleteMedicineTypeByIdResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                }
            );
        });
    }

    public override async Task<DeleteMedicineTypeByIdHttpResponse> ExecuteAsync(
        DeleteMedicineTypeByIdRequest req,
        CancellationToken ct
    )
    {
        var appResponse = await req.ExecuteAsync(ct: ct);

        var httpResponse = DeleteMedicineTypeByIdHttpResponseMapper
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
