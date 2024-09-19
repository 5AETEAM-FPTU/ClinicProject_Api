using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Threading;
using Clinic.WebAPI.EndPoints.Users.UpdateDoctorDescription.HttpResponseMapper;
using Clinic.Application.Features.Users.UpdateDoctorAchievement;
using Clinic.WebAPI.EndPoints.Users.UpdateDoctorAchievement.HttpResponseMapper;

namespace Clinic.WebAPI.EndPoints.Users.UpdateDoctorAchievement;

public class UpdateDoctorAchievementEndpoint : Endpoint<UpdateDoctorAchievementByIdRequest, UpdateDoctorAchievementHttpResponse>
{
    public override void Configure()
    {
        Patch("doctor/achievement");
        AuthSchemes(JwtBearerDefaults.AuthenticationScheme);
        DontThrowIfValidationFails();
        Description(builder =>
        {
            builder.ClearDefaultProduces(StatusCodes.Status400BadRequest);
        });
        Summary(summary =>
        {
            summary.Summary = "Endpoint to update Doctor achievement";
            summary.Description = "This endpoint allows users to update doctor achievement.";
            summary.Response<UpdateDoctorDescriptionHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = UpdateDoctorAchievementByIdResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                }
            );
        });
    }

    public override async Task<UpdateDoctorAchievementHttpResponse> ExecuteAsync
        (UpdateDoctorAchievementByIdRequest req, 
        CancellationToken ct)
    {

        var appResponse = await req.ExecuteAsync(ct: ct); // Assuming the actual update logic is in AppRequest.

        var httpResponse = UpdateDoctorAchievementHttpResponseMapper
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
