using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Threading.Tasks;
using System.Threading;
using Clinic.Application.Features.Admin.UpdateMedicine;
using Clinic.WebAPI.EndPoints.Admin.UpdateMedicine.HttpResponseMapper;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Admin.UpdateMedicine;

public class UpdateMedicineEndpoint : Endpoint<UpdateMedicineRequest, UpdateMedicineHttpResponse>
{
    public override void Configure()
    {
        Patch("admin/medicine/update/{medicineId}");
        AuthSchemes(JwtBearerDefaults.AuthenticationScheme);
        DontThrowIfValidationFails();
        Description(builder =>
        {
            builder.ClearDefaultProduces(StatusCodes.Status400BadRequest);
        });
        Summary(summary =>
        {
            summary.Summary = "Endpoint to update medicine information";
            summary.Description = "This endpoint allows doctor, admin, staff to update medicine information.";
            summary.Response<UpdateMedicineHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = UpdateMedicineResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                }
            );
        });
    }

    public override async Task<UpdateMedicineHttpResponse> ExecuteAsync
        (UpdateMedicineRequest req,
        CancellationToken ct)
    {

        var appResponse = await req.ExecuteAsync(ct: ct); // Assuming the actual update logic is in AppRequest.

        var httpResponse = UpdateMedicineHttpResponseMapper
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

