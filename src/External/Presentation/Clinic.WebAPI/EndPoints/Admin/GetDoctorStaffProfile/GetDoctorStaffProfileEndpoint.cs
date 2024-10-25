using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Features.Admin.GetDoctorStaffProfile;
using Clinic.WebAPI.EndPoints.Admin.GetAvailableMedicines.HttpResponseMapper;
using Clinic.WebAPI.EndPoints.Admin.GetDoctorStaffProfile.HttpResponseMapper;
using Clinic.WebAPI.EndPoints.Doctors.GetAllDoctorForStaff.HttpResponseMapper;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Admin.GetDoctorStaffProfile;

public class GetDoctorStaffProfileEndpoint
    : Endpoint<GetDoctorStaffProfileRequest, GetDoctorStaffProfileHttpResponse>
{
    public override void Configure()
    {
        Get(routePatterns: "admin/profile/doctor-staff");
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
            summary.Response<GetDoctorStaffProfileHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = GetDoctorStaffProfileResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                }
            );
        });
    }

    public override async Task<GetDoctorStaffProfileHttpResponse> ExecuteAsync(
        GetDoctorStaffProfileRequest req,
        CancellationToken ct
    )
    {
        var appResponse = await req.ExecuteAsync(ct: ct);

        var httpResponse = GetDoctorStaffProfileHttpResponseMapper
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
