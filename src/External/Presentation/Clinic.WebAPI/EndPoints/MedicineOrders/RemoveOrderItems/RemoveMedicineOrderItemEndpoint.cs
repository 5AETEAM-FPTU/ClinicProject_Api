using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Features.MedicineOrders.RemoveOrderItems;
using Clinic.WebAPI.EndPoints.MedicineOrders.RemoveOrderItems.HttpResponseMapper;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.MedicineOrders.RemoveOrderItems;

/// <summary>
///     RemoveMedicineOrderItem endpoint
/// </summary>
public class RemoveMedicineOrderItemEndpoint
    : Endpoint<RemoveMedicineOrderItemRequest, RemoveMedicineOrderItemHttpResponse>
{
    public override void Configure()
    {
        Delete("medicine-order/item/remove/{medicineOrderId}/{medicineId}");
        AuthSchemes(authSchemeNames: JwtBearerDefaults.AuthenticationScheme);
        DontThrowIfValidationFails();
        Description(builder =>
        {
            builder.ClearDefaultProduces(StatusCodes.Status400BadRequest);
        });
        Summary(summary =>
        {
            summary.Summary = "Endpoint for doctor/staff to remove medicine item of prescription.";
            summary.Description = "This endpoint allows doctor/staff to remove medicine item of prescription.";
            summary.Response<RemoveMedicineOrderItemHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = RemoveMedicineOrderItemResponseStatusCode.OPERATION_SUCCESS.ToAppCode()
                }
            );
        });
    }

    public override async Task<RemoveMedicineOrderItemHttpResponse> ExecuteAsync(
        RemoveMedicineOrderItemRequest req,
        CancellationToken ct
    )
    {
        var appResponse = await req.ExecuteAsync(ct: ct);

        var httpResponse = RemoveMedicineOrderItemHttpResponseMapper
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
