using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Threading.Tasks;
using System.Threading;
using Clinic.Application.Features.Admin.GetAllMedicine;
using Clinic.WebAPI.EndPoints.Admin.GetAllMedicine.HttpResponseMapper;
using Microsoft.AspNetCore.Http;
using Clinic.Application.Features.Auths.Login;

namespace Clinic.WebAPI.EndPoints.Admin.GetAllMedicine;

/// <summary>
///     GetAllMedicine endpoint.
/// </summary>
internal sealed class GetAllMedicineEndpoint
    : Endpoint<GetAllMedicineRequest, GetAllMedicineHttpResponse>
{
    public override void Configure()
    {
        Get(routePatterns: "admin/medicine/all");
        AuthSchemes(authSchemeNames: JwtBearerDefaults.AuthenticationScheme);
        DontThrowIfValidationFails();
        Description(builder: builder =>
        {
            builder.ClearDefaultProduces(statusCodes: StatusCodes.Status400BadRequest);
        });
        Summary(endpointSummary: summary =>
        {
            summary.Summary = "Endpoint for Admin feature";
            summary.Description = "This endpoint is used for display all medicines.";
            summary.Response<GetAllMedicineHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = LoginResponseStatusCode.OPERATION_SUCCESS.ToAppCode()
                }
            );
        });
    }

    public override async Task<GetAllMedicineHttpResponse> ExecuteAsync(
        GetAllMedicineRequest req,
        CancellationToken ct
    )
    {
        //// Get app feature response.
        //var stateBag = ProcessorState<GetAllDoctorForBookingStateBag>();

        var appResponse = await req.ExecuteAsync(ct: ct);

        var httpResponse = GetAllMedicineHttpResponseMapper
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
