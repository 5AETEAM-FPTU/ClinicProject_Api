using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Features.MedicineOrders.UpdateOrderItems;
using Clinic.WebAPI.EndPoints.MedicineOrders.UpdateNoteMedicineOrder.HttpResponseMapper;
using Clinic.WebAPI.EndPoints.MedicineOrders.UpdateOrderItems.HttpResponseMapper;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.MedicineOrders.UpdateOrderItems;

/// <summary>
///     UpdateMedicineOrderItem endpoint
/// </summary>
public class UpdateMedicineOrderItemEndpoint
    : Endpoint<UpdateMedicineOrderItemRequest, UpdateMedicineOrderItemHttpResponse>
{
    public override void Configure()
    {
        Patch("medicine-order/item/update");
        AuthSchemes(authSchemeNames: JwtBearerDefaults.AuthenticationScheme);
        DontThrowIfValidationFails();
        Description(builder =>
        {
            builder.ClearDefaultProduces(StatusCodes.Status400BadRequest);
        });
        Summary(summary =>
        {
            summary.Summary = "Endpoint to get detail of service indication (order).";
            summary.Description = "This endpoint allows user to get detail of service indication.";
            summary.Response<UpdateMedicineOrderItemHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = UpdateMedicineOrderItemResponseStatusCode.OPERATION_SUCCESS.ToAppCode()
                }
            );
        });
    }

    public override async Task<UpdateMedicineOrderItemHttpResponse> ExecuteAsync(
        UpdateMedicineOrderItemRequest req,
        CancellationToken ct
    )
    {
        var appResponse = await req.ExecuteAsync(ct: ct);

        var httpResponse = UpdateMedicineOrderItemHttpResponseMapper
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
