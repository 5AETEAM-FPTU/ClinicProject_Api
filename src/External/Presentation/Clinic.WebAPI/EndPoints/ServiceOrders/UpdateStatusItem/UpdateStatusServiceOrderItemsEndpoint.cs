using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Features.ServiceOrders.UpdateStatusItem;
using Clinic.WebAPI.EndPoints.ServiceOrders.UpdateStatusItem.HttpResponseMapper;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.ServiceOrders.UpdateStatusItem;

/// <summary>
///     UpdateStatusServiceOrderItems endpoint
/// </summary>
public class UpdateStatusServiceOrderItemsEndpoint
    : Endpoint<UpdateStatusServiceOrderItemsRequest, UpdateStatusServiceOrderItemsHttpResponse>
{
    public override void Configure()
    {
        Patch("service-order/status/update");
        AuthSchemes(authSchemeNames: JwtBearerDefaults.AuthenticationScheme);
        DontThrowIfValidationFails();
        Description(builder =>
        {
            builder.ClearDefaultProduces(StatusCodes.Status400BadRequest);
        });
        Summary(summary =>
        {
            summary.Summary = "Endpoint for doctor/staff to change status of service order item after updating result of service.";
            summary.Description = "This endpoint allows doctor/staff to change status of service order item to 'isUpdate = true' after updating result of service";
            summary.Response<UpdateStatusServiceOrderItemsHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = UpdateStatusServiceOrderItemsResponseStatusCode.OPERATION_SUCCESS.ToAppCode()
                }
            );
        });
    }

    public override async Task<UpdateStatusServiceOrderItemsHttpResponse> ExecuteAsync(
        UpdateStatusServiceOrderItemsRequest req,
        CancellationToken ct
    )
    {
        var appResponse = await req.ExecuteAsync(ct: ct);

        var httpResponse = UpdateStatusServiceOrderItemsHttpResponseMapper
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
