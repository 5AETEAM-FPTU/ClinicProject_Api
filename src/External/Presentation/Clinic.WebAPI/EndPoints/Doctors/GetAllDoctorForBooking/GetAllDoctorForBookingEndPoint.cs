using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Threading.Tasks;
using System.Threading;
using Clinic.WebAPI.EndPoints.Doctors.GetAllDoctorForBooking.HttpResponseMapper;
using Microsoft.AspNetCore.Http;
using Clinic.Application.Features.Auths.Login;
using Clinic.WebAPI.EndPoints.Doctors.GetAllDoctorForBooking.Common;
using Clinic.Application.Features.Doctors.GetAllDoctorForBooking;
using Clinic.WebAPI.EndPoints.Doctors.GetMedicalReportById.HttpResponseMapper;

namespace Clinic.WebAPI.EndPoints.Doctors.GetAllDoctorForBooking;

/// <summary>
///     GetAllDoctorForBooking endpoint.
/// </summary>
internal sealed class GetAllDoctorForBookingEndpoint
    : Endpoint<GetAllDoctorForBookingRequest, GetAllDoctorForBookingHttpResponse>
{
    public override void Configure()
    {
        Get(routePatterns: "doctor/getAllDoctorForBooking");
        AuthSchemes(authSchemeNames: JwtBearerDefaults.AuthenticationScheme);
        DontThrowIfValidationFails();
        Description(builder: builder =>
        {
            builder.ClearDefaultProduces(statusCodes: StatusCodes.Status400BadRequest);
        });
        Summary(endpointSummary: summary =>
        {
            summary.Summary = "Endpoint for Doctor feature";
            summary.Description = "This endpoint is used for display doctor for user booking.";
            summary.Response<GetAllDoctorForBookingHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = LoginResponseStatusCode.OPERATION_SUCCESS.ToAppCode()
                }
            );
        });
    }

    public override async Task<GetAllDoctorForBookingHttpResponse> ExecuteAsync(
        GetAllDoctorForBookingRequest req,
        CancellationToken ct
    )
    {
        //// Get app feature response.
        //var stateBag = ProcessorState<GetAllDoctorForBookingStateBag>();

        var appResponse = await req.ExecuteAsync(ct: ct);

        var httpResponse = GetAllDoctorForBookingHttpResponseMapper
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
