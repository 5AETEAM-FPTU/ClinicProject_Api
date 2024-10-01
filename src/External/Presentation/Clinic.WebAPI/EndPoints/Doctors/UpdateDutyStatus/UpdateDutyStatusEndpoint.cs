using Clinic.Application.Features.Doctors.UpdateDoctorDescription;
using Clinic.Application.Features.Doctors.UpdateDutyStatus;
using Clinic.WebAPI.EndPoints.Doctors.UpdateDoctorDescription.HttpResponseMapper;
using Clinic.WebAPI.EndPoints.Doctors.UpdateDutyStatus.HttpResponseMapper;
using FastEndpoints;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
namespace Clinic.WebAPI.EndPoints.Doctors.UpdateDutyStatus
{
    public class UpdateDutyStatusEndpoint : Endpoint<UpdateDutyStatusRequest, UpdateDutyStatusHttpResponse>
    {
        public override void Configure()
        {
            Patch("/doctor/duty");
            AuthSchemes(JwtBearerDefaults.AuthenticationScheme);
            DontThrowIfValidationFails();
            Description(builder =>
            {
                builder.ClearDefaultProduces(StatusCodes.Status400BadRequest);
            });
            Summary(summary =>
            {
                summary.Summary = "Endpoint to update duty status of Doctor description";
                summary.Description = "This endpoint allows doctor to update doctor's duty status description.";
                summary.Response<UpdateDutyStatusHttpResponse>(
                    description: "Represent successful operation response.",
                    example: new()
                    {
                        HttpCode = StatusCodes.Status200OK,
                        AppCode = UpdateDutyStatusResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                    }
                );
            });
        }
        public override async Task<UpdateDutyStatusHttpResponse> ExecuteAsync
        (UpdateDutyStatusRequest req,
        CancellationToken ct)
        {
            var appResponse = await req.ExecuteAsync(ct: ct); // Assuming the actual update logic is in AppRequest.

            var httpResponse = UpdateDutyStatusHttpResponseMapper
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
}
