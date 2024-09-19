using Clinic.Application.Commons.Abstractions.UpdatePrivateDoctorInfoById;
using Clinic.WebAPI.EndPoints.Users.UpdatePrivateDoctorInfo.HttpResponseMapper;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Threading;
using Clinic.Application.Features.Users.UpdatePrivateDoctorInfoById;
using Clinic.WebAPI.Commons.Behaviors.Validation;

namespace Clinic.WebAPI.EndPoints.Users.UpdatePrivateDoctorInfo;

public class UpdatePrivateDoctorInfoEndpoint : Endpoint<UpdatePrivateDoctorInfoByIdRequest, UpdatePrivateDoctorInfoHttpResponse>
{
    public override void Configure()
    {
        Patch("doctor/private-info");
        PreProcessor<ValidationPreProcessor<UpdatePrivateDoctorInfoByIdRequest>>();
        AuthSchemes(JwtBearerDefaults.AuthenticationScheme);
        DontThrowIfValidationFails();
        Description(builder =>
        {
            builder.ClearDefaultProduces(StatusCodes.Status400BadRequest);
        });
        Summary(summary =>
        {
            summary.Summary = "Endpoint to update Doctor private information";
            summary.Description = "This endpoint allows users to update doctor private information.";
            summary.Response<UpdatePrivateDoctorInfoHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = UpdatePrivateDoctorInfoByIdResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                }
            );
        });
    }

    public override async Task<UpdatePrivateDoctorInfoHttpResponse> ExecuteAsync
        (UpdatePrivateDoctorInfoByIdRequest req, 
        CancellationToken ct)
    {

        var appResponse = await req.ExecuteAsync(ct: ct); // Assuming the actual update logic is in AppRequest.

        var httpResponse = UpdatePrivateDoctorInfoHttpResponseMapper
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
