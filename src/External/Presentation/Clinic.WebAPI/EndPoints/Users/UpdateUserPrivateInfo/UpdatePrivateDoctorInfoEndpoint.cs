using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Threading;
using Clinic.WebAPI.Commons.Behaviors.Validation;
using Clinic.Application.Features.Doctors.UpdatePrivateDoctorInfo;
using Clinic.WebAPI.EndPoints.Doctors.UpdatePrivateDoctorInfo.HttpResponseMapper;
using Clinic.Application.Features.Users.UpdateUserPrivateInfo;
using Clinic.WebAPI.EndPoints.Doctors.UpdateUserPrivateInfo.HttpResponseMapper;

namespace Clinic.WebAPI.EndPoints.Doctors.UpdateUserPrivateInfo;

public class UpdatePrivateDoctorInfoEndpoint : Endpoint<UpdateUserPrivateInfoRequest, UpdateUserPrivateInfoHttpResponse>
{
    public override void Configure()
    {
        Patch("user/private-info");
        AuthSchemes(JwtBearerDefaults.AuthenticationScheme);
        PreProcessor<ValidationPreProcessor<UpdateUserPrivateInfoRequest>>();
        DontThrowIfValidationFails();
        Description(builder =>
        {
            builder.ClearDefaultProduces(StatusCodes.Status400BadRequest);
        });
        Summary(summary =>
        {
            summary.Summary = "Endpoint to update User private information";
            summary.Description = "This endpoint allows users to update user private information.";
            summary.Response<UpdatePrivateDoctorInfoHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = UpdateUserPrivateInfoResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                }
            );
        });
    }

    public override async Task<UpdateUserPrivateInfoHttpResponse> ExecuteAsync
        (UpdateUserPrivateInfoRequest req,
        CancellationToken ct)
    {

        var appResponse = await req.ExecuteAsync(ct: ct); // Assuming the actual update logic is in AppRequest.

        var httpResponse = UpdateUserPrivateInfoHttpResponseMapper
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
