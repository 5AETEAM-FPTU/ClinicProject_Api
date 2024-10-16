using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Features.MedicineOrders.OrderMedicines;
using Clinic.WebAPI.EndPoints.MedicineOrders.OrderMedicines.HttpResponseMapper;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.MedicineOrders.OrderMedicines;

/// <summary>
///     GetMedicineOrderItems endpoint
/// </summary>
public class OrderMedicinesEndpoint
    : Endpoint<OrderMedicinesRequest, OrderMedicinesHttpResponse>
{
    public override void Configure()
    {
        Post("medicine-order/order");
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
            summary.Response<OrderMedicinesHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = OrderMedicinesResponseStatusCode.OPERATION_SUCCESS.ToAppCode()
                }
            );
        });
    }

    public override async Task<OrderMedicinesHttpResponse> ExecuteAsync(
        OrderMedicinesRequest req,
        CancellationToken ct
    )
    {
        var appResponse = await req.ExecuteAsync(ct: ct);

        var httpResponse = OrderMedicinesHttpResponseMapper
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
