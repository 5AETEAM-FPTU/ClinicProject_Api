using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Features.ServiceOrders.AddOrderService;
using Clinic.WebAPI.EndPoints.ServiceOrders.AddOrderService.HttpResponseMapper;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.ServiceOrders.AddOrderService;

/// <summary>
///     GetServiceOrderItems endpoint
/// </summary>
public class AddOrderServiceEndpoint
    : Endpoint<AddOrderServiceRequest, AddOrderServiceHttpResponse>
{
    public override void Configure()
    {
        Post("service-order/add");
        AuthSchemes(authSchemeNames: JwtBearerDefaults.AuthenticationScheme);
        DontThrowIfValidationFails();
        Description(builder =>
        {
            builder.ClearDefaultProduces(StatusCodes.Status400BadRequest);
        });
        Summary(summary =>
        {
            summary.Summary = "Endpoint for staff/doctor to add list services into service's order. (order service)";
            summary.Description = "This endpoint allows doctor/staff to add list services into service's order.";
            summary.Response<AddOrderServiceHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = AddOrderServiceResponseStatusCode.OPERATION_SUCCESS.ToAppCode()
                }
            );
        });
    }

    public override async Task<AddOrderServiceHttpResponse> ExecuteAsync(
        AddOrderServiceRequest req,
        CancellationToken ct
    )
    {
        var appResponse = await req.ExecuteAsync(ct: ct);

        var httpResponse = AddOrderServiceHttpResponseMapper
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
