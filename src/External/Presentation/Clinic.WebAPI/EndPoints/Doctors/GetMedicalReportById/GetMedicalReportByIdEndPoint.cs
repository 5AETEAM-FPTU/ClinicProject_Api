using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Threading.Tasks;
using System.Threading;
using Clinic.WebAPI.EndPoints.Doctors.GetMedicalReportById.HttpResponseMapper;
using Microsoft.AspNetCore.Http;
using Clinic.Application.Features.Auths.Login;
using Clinic.Application.Features.Doctors.GetMedicalReportById;

namespace Clinic.WebAPI.EndPoints.Doctors.GetMedicalReportById;

/// <summary>
///     GetMedicalReportById endpoint.
/// </summary>
internal sealed class GetMedicalReportByIdEndpoint
    : Endpoint<GetMedicalReportByIdRequest, GetMedicalReportByIdHttpResponse>
{
    public override void Configure()
    {
        Get(routePatterns: "doctor/getMedicalReportById");
        AuthSchemes(authSchemeNames: JwtBearerDefaults.AuthenticationScheme);
        DontThrowIfValidationFails();
        Description(builder: builder =>
        {
            builder.ClearDefaultProduces(statusCodes: StatusCodes.Status400BadRequest);
        });
        Summary(endpointSummary: summary =>
        {
            summary.Summary = "Endpoint for Doctor feature";
            summary.Description = "This endpoint is used for display profile user medical report .";
            summary.Response<GetMedicalReportByIdHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = LoginResponseStatusCode.OPERATION_SUCCESS.ToAppCode()
                }
            );
        });
    }

    public override async Task<GetMedicalReportByIdHttpResponse> ExecuteAsync(
          GetMedicalReportByIdRequest req,
          CancellationToken ct
      )
    {
        var appResponse = await req.ExecuteAsync(ct: ct);

        var httpResponse = GetMedicalReportByIdHttpResponseMapper
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

