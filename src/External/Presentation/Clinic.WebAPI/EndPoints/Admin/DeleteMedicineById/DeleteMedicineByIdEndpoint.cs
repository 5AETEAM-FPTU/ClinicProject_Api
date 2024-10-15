using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Features.Admin.DeleteMedicineById;
using Clinic.WebAPI.EndPoints.Admin.DeleteMedicineById.HttpResponseMapper;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Admin.DeleteMedicineById;

/// <summary>
///     DeleteMedicineById endpoint
/// </summary>
public class DeleteMedicineByIdEndpoint
    : Endpoint<DeleteMedicineByIdRequest, DeleteMedicineByIdHttpResponse>
{
    public override void Configure()
    {
        Delete("admin/medicine/remove/{medicineId}");
        AuthSchemes(JwtBearerDefaults.AuthenticationScheme);
        DontThrowIfValidationFails();
        Description(builder =>
        {
            builder.ClearDefaultProduces(StatusCodes.Status400BadRequest);
        });
        Summary(summary =>
        {
            summary.Summary = "Endpoint for admin";
            summary.Description = "This endpoint allows admin to remove specific medicine.";
            summary.Response<DeleteMedicineByIdHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = DeleteMedicineByIdResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                }
            );
        });
    }

    public override async Task<DeleteMedicineByIdHttpResponse> ExecuteAsync(
        DeleteMedicineByIdRequest req,
        CancellationToken ct
    )
    {
        var appResponse = await req.ExecuteAsync(ct: ct);

        var httpResponse = DeleteMedicineByIdHttpResponseMapper
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
