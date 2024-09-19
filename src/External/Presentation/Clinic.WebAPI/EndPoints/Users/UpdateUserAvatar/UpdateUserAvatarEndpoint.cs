using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Threading;
using Clinic.WebAPI.EndPoints.Users.UpdateUserAvatar.HttpResponseMapper;
using Clinic.Application.Features.Users.UpdateUserAvatar;

namespace Clinic.WebAPI.EndPoints.Users.UpdateUserAvatar;

public class UpdateUserAvatarEndpoint : Endpoint<UpdateUserAvatarRequest, UpdateUserAvatarHttpResponse>
{
    public override void Configure()
    {
        Patch("user/avatar");
        AuthSchemes(JwtBearerDefaults.AuthenticationScheme);
        DontThrowIfValidationFails();
        Description(builder =>
        {
            builder.ClearDefaultProduces(StatusCodes.Status400BadRequest);
        });
        Summary(summary =>
        {
            summary.Summary = "Endpoint to update User avatar";
            summary.Description = "This endpoint allows users to update user avatar.";
            summary.Response<UpdateUserAvatarHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = UpdateUserAvatarResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                }
            );
        });
    }

    public override async Task<UpdateUserAvatarHttpResponse> ExecuteAsync
        (UpdateUserAvatarRequest req, 
        CancellationToken ct)
    {

        var appResponse = await req.ExecuteAsync(ct: ct); // Assuming the actual update logic is in AppRequest.

        var httpResponse = UpdateUserAvatarHttpResponseMapper
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
