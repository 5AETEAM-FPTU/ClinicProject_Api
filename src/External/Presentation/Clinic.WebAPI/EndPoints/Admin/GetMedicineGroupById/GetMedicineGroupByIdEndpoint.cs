using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Threading.Tasks;
using System.Threading;
using Clinic.Application.Features.Admin.GetMedicineGroupById;
using Clinic.WebAPI.EndPoints.Admin.GetMedicineGroupById.HttpResponseMapper;
using Microsoft.AspNetCore.Http;
using Clinic.Application.Features.Auths.Login;

namespace Clinic.WebAPI.EndPoints.Admin.GetMedicineGroupById;

/// <summary>
///     GetMedicineGroupById endpoint.
/// </summary>
internal sealed class GetMedicineGroupByIdEndpoint
    : Endpoint<GetMedicineGroupByIdRequest, GetMedicineGroupByIdHttpResponse>
{
    public override void Configure()
    {
        Get(routePatterns: "admin/medicineGroup");
        AuthSchemes(authSchemeNames: JwtBearerDefaults.AuthenticationScheme);
        DontThrowIfValidationFails();
        Description(builder: builder =>
        {
            builder.ClearDefaultProduces(statusCodes: StatusCodes.Status400BadRequest);
        });
        Summary(endpointSummary: summary =>
        {
            summary.Summary = "Endpoint for Admin feature";
            summary.Description = "This endpoint is used for display medicine group by id.";
            summary.Response<GetMedicineGroupByIdHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = LoginResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                }
            );
        });
    }

    public override async Task<GetMedicineGroupByIdHttpResponse> ExecuteAsync(
        GetMedicineGroupByIdRequest req,
        CancellationToken ct
    )
    {
        var appResponse = await req.ExecuteAsync(ct: ct);

        var httpResponse = GetMedicineGroupByIdHttpResponseMapper
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
