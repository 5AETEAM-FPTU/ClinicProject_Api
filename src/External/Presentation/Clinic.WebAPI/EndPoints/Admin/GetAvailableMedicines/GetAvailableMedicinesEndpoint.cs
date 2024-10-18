using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Features.Admin.GetAvailableMedicines;
using Clinic.Application.Features.Auths.Login;
using Clinic.WebAPI.EndPoints.Admin.GetAvailableMedicines.HttpResponseMapper;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Admin.GetAvailableMedicines;

/// <summary>
///     GetAvailableMedicines endpoint.
/// </summary>
internal sealed class GetAvailableMedicinesEndpoint
    : Endpoint<GetAvailableMedicinesRequest, GetAvailableMedicinesHttpResponse>
{
    public override void Configure()
    {
        Get(routePatterns: "admin/medicine/available");
        AuthSchemes(authSchemeNames: JwtBearerDefaults.AuthenticationScheme);
        DontThrowIfValidationFails();
        Description(builder: builder =>
        {
            builder.ClearDefaultProduces(statusCodes: StatusCodes.Status400BadRequest);
        });
        Summary(endpointSummary: summary =>
        {
            summary.Summary = "Endpoint for Admin feature";
            summary.Description = "This endpoint is used for display all medicines available.";
            summary.Response<GetAvailableMedicinesHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = LoginResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                }
            );
        });
    }

    public override async Task<GetAvailableMedicinesHttpResponse> ExecuteAsync(
        GetAvailableMedicinesRequest req,
        CancellationToken ct
    )
    {
        //// Get app feature response.
        //var stateBag = ProcessorState<GetAllDoctorForBookingStateBag>();

        var appResponse = await req.ExecuteAsync(ct: ct);

        var httpResponse = GetAvailableMedicinesHttpResponseMapper
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
