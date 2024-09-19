using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Threading;
using Clinic.Application.Features.Doctors.UpdatePrivateDoctorInfo;
using Clinic.Application.Features.Users.UpdateUserPrivateInfo;
using Clinic.Application.Features.Users.UpdateUserDesciption;
using Clinic.WebAPI.EndPoints.Doctors.UpdateUserDescription.HttpResponseMapper;

namespace Clinic.WebAPI.EndPoints.Doctors.UpdateUserDescription;

public class UpdateUserDescriptionEndpoint : Endpoint<UpdateUserDesciptionRequest, UpdateUserDescriptionHttpResponse>
{
    public override void Configure()
    {
        Patch("user/description");
        AuthSchemes(JwtBearerDefaults.AuthenticationScheme);
        DontThrowIfValidationFails();
        Description(builder =>
        {
            builder.ClearDefaultProduces(StatusCodes.Status400BadRequest);
        });
        Summary(summary =>
        {
            summary.Summary = "Endpoint to update User description";
            summary.Description = "This endpoint allows users to update user description.";
            summary.Response<UpdateUserDescriptionHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = UpdateUserPrivateInfoResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                }
            );
        });
    }

    public override async Task<UpdateUserDescriptionHttpResponse> ExecuteAsync
        (UpdateUserDesciptionRequest req,
        CancellationToken ct)
    {

        var appResponse = await req.ExecuteAsync(ct: ct); // Assuming the actual update logic is in AppRequest.

        var httpResponse = UpdateUserDescriptionHttpResponseMapper
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
