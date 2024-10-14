using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Threading.Tasks;
using System.Threading;
using Clinic.Application.Features.Admin.UpdateMedicineGroupById;
using Clinic.WebAPI.EndPoints.Admin.UpdateMedicineGroupById.HttpResponseMapper;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Admin.UpdateMedicineGroupById;

public class UpdateMedicineGroupByIdEndpoint : Endpoint<UpdateMedicineGroupByIdRequest, UpdateMedicineGroupByIdHttpResponse>
{
    public override void Configure()
    {
        Patch("admin/medicineGroup/update/{medicineGroupId}");
        AuthSchemes(JwtBearerDefaults.AuthenticationScheme);
        DontThrowIfValidationFails();
        Description(builder =>
        {
            builder.ClearDefaultProduces(StatusCodes.Status400BadRequest);
        });
        Summary(summary =>
        {
            summary.Summary = "Endpoint to update medicine information";
            summary.Description = "This endpoint allows doctor, admin, staff to update medicine group information.";
            summary.Response<UpdateMedicineGroupByIdHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = UpdateMedicineGroupByIdResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                }
            );
        });
    }

    public override async Task<UpdateMedicineGroupByIdHttpResponse> ExecuteAsync
        (UpdateMedicineGroupByIdRequest req,
        CancellationToken ct)
    {

        var appResponse = await req.ExecuteAsync(ct: ct); // Assuming the actual update logic is in AppRequest.

        var httpResponse = UpdateMedicineGroupByIdHttpResponseMapper
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
