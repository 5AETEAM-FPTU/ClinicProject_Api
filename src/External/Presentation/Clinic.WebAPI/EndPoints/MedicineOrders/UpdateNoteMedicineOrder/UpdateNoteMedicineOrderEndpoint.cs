using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Features.MedicineOrders.UpdateNoteMedicineOrder;
using Clinic.WebAPI.EndPoints.MedicineOrders.UpdateNoteMedicineOrder.HttpResponseMapper;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.MedicineOrders.UpdateNoteMedicineOrder;

/// <summary>
///     UpdateNoteMedicineOrder endpoint
/// </summary>
public class UpdateNoteMedicineOrderEndpoint
    : Endpoint<UpdateNoteMedicineOrderRequest, UpdateNoteMedicineOrderHttpResponse>
{
    public override void Configure()
    {
        Patch("medicine-order/note/update");
        AuthSchemes(authSchemeNames: JwtBearerDefaults.AuthenticationScheme);
        DontThrowIfValidationFails();
        Description(builder =>
        {
            builder.ClearDefaultProduces(StatusCodes.Status400BadRequest);
        });
        Summary(summary =>
        {
            summary.Summary = "Endpoint for doctor/staff to update note in medical order's note (advise of doctor).";
            summary.Description = "This endpoint allows doctor/staff to update note of medicine indication.";
            summary.Response<UpdateNoteMedicineOrderHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = UpdateNoteMedicineOrderResponseStatusCode.OPERATION_SUCCESS.ToAppCode()
                }
            );
        });
    }

    public override async Task<UpdateNoteMedicineOrderHttpResponse> ExecuteAsync(
        UpdateNoteMedicineOrderRequest req,
        CancellationToken ct
    )
    {
        var appResponse = await req.ExecuteAsync(ct: ct);

        var httpResponse = UpdateNoteMedicineOrderHttpResponseMapper
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
