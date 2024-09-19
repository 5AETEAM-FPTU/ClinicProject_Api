using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Threading;
using Clinic.Application.Features.Users.UpdatePrivateDoctorInfoById;
using Clinic.Application.Commons.Abstractions.UpdateDoctorDescription;
using Clinic.WebAPI.EndPoints.Users.UpdateDoctorDescription.HttpResponseMapper;
using Clinic.Application.Features.Users.UpdateDoctorDescription;

namespace Clinic.WebAPI.EndPoints.Users.UpdateDoctorDescription;

public class UpdateDoctorDescriptionEndpoint : Endpoint<UpdateDoctorDescriptionByIdRequest, UpdateDoctorDescriptionHttpResponse>
{
    public override void Configure()
    {
        Patch("doctor/description");
        AuthSchemes(JwtBearerDefaults.AuthenticationScheme);
        DontThrowIfValidationFails();
        Description(builder =>
        {
            builder.ClearDefaultProduces(StatusCodes.Status400BadRequest);
        });
        Summary(summary =>
        {
            summary.Summary = "Endpoint to update Doctor description";
            summary.Description = "This endpoint allows users to update doctor description.";
            summary.Response<UpdateDoctorDescriptionHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = UpdateDoctorDescriptionByIdResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                }
            );
        });
    }

    public override async Task<UpdateDoctorDescriptionHttpResponse> ExecuteAsync
        (UpdateDoctorDescriptionByIdRequest req, 
        CancellationToken ct)
    {

        var appResponse = await req.ExecuteAsync(ct: ct); // Assuming the actual update logic is in AppRequest.

        var httpResponse = UpdateDoctorDescriptionHttpResponseMapper
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
