using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Features.Doctors.GetRecentMedicalReportByUserId;
using Clinic.WebAPI.EndPoints.Doctors.GetRecentMedicalReportByUserId.HttpResponseMapper;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Doctors.GetRecentMedicalReportByUserId;

/// <summary>
///     GetRecentMedicalReportByUserId endpoint
/// </summary>
public class GetRecentBookedAppointmentsEndpoint
    : Endpoint<GetRecentMedicalReportByUserIdRequest, GetRecentMedicalReportByUserIdHttpResponse>
{
    public override void Configure()
    {
        Get("/doctor/medicalreport/recent/{UserId}");
        AuthSchemes(authSchemeNames: JwtBearerDefaults.AuthenticationScheme);
        DontThrowIfValidationFails();
        Description(builder =>
        {
            builder.ClearDefaultProduces(StatusCodes.Status400BadRequest);
        });
        Summary(summary =>
        {
            summary.Summary = "Endpoint for doctor/staff.";
            summary.Description = "This endpoint allows doctor to get user recent medical reports.";
            summary.Response<GetRecentMedicalReportByUserIdHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode =
                        GetRecentMedicalReportByUserIdResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                }
            );
        });
    }

    public override async Task<GetRecentMedicalReportByUserIdHttpResponse> ExecuteAsync(
        GetRecentMedicalReportByUserIdRequest req,
        CancellationToken ct
    )
    {
        var appResponse = await req.ExecuteAsync(ct: ct);

        var httpResponse = GetRecentMedicalReportByUserIdHttpResponseMapper
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
