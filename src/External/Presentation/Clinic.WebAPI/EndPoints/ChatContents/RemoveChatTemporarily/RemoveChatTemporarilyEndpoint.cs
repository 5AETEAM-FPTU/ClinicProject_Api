using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Features.ChatContents.RemoveChatContentTemporarily;
using Clinic.WebAPI.EndPoints.ChatContents.RemoveChatTemporarily.HttpResponseMapper;
using Clinic.WebAPI.EndPoints.ChatRooms.RemoveChatTemporarily.HttpResponseMapper;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.ChatRooms.RemoveChatTemporarily;

/// <summary>
///     Endpoint for RemoveChatTemporarily.
/// </summary>
public class RemoveChatTemporarilyEndpoint
    : Endpoint<RemoveChatContentTemporarilyRequest, RemoveChatTemporarilyHttpResponse>
{
    public override void Configure()
    {
        Delete("chat-content/temporarily/{chatContentId}");
        AuthSchemes(JwtBearerDefaults.AuthenticationScheme);
        DontThrowIfValidationFails();
        Description(builder =>
        {
            builder.ClearDefaultProduces(StatusCodes.Status400BadRequest);
        });
        Summary(summary =>
        {
            summary.Summary = "Endpoint for remove chat temporarily.";
            summary.Description =
                "This endpoint allow doctor assign the patient who they want to remove their message.";
            summary.Response<RemoveChatTemporarilyHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode =
                        RemoveChatContentTemporarilyResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                }
            );
        });
    }

    public override async Task<RemoveChatTemporarilyHttpResponse> ExecuteAsync(
        RemoveChatContentTemporarilyRequest req,
        CancellationToken ct
    )
    {
        var appResponse = await req.ExecuteAsync(ct: ct);

        var httpResponse = RemoveChatTemporarilyHttpResponseMapper
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
