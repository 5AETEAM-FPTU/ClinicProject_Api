using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Features.MedicineOrders.GetMedicineOrderItems;
using Clinic.WebAPI.EndPoints.MedicineOrders.GetMedicineOrderItems.HttpResponseMapper;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.MedicineOrders.GetMedicineOrderItems;

/// <summary>
///     GetMedicineOrderItems endpoint
/// </summary>
public class GetMedicineOrderItemsEndpoint
    : Endpoint<GetMedicineOrderItemsRequest, GetMedicineOrderItemsHttpResponse>
{
    public override void Configure()
    {
        Get("medicine-order/detail");
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
            summary.Response<GetMedicineOrderItemsHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = GetMedicineOrderItemsResponseStatusCode.OPERATION_SUCCESS.ToAppCode()
                }
            );
        });
    }

    public override async Task<GetMedicineOrderItemsHttpResponse> ExecuteAsync(
        GetMedicineOrderItemsRequest req,
        CancellationToken ct
    )
    {
        var appResponse = await req.ExecuteAsync(ct: ct);

        var httpResponse = GetMedicineOrderItemsHttpResponseMapper
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
