using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Features.ChatContents.GetChatsByChatRoomId;
using Clinic.WebAPI.EndPoints.ChatContents.GetChatsByChatRoomId.HttpResponseMapper;
using Clinic.WebAPI.EndPoints.ChatRooms.GetChatsByChatRoomId.HttpResponseMapper;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.ChatRooms.GetChatsByChatRoomId;

/// <summary>
///     Endpoint for GetChatsByChatRoomId.
/// </summary>
public class GetChatsByChatRoomIdEndpoint
    : Endpoint<GetChatsByChatRoomIdRequest, GetChatsByChatRoomIdHttpResponse>
{
    public override void Configure()
    {
        Get("chat-content/message");
        AuthSchemes(JwtBearerDefaults.AuthenticationScheme);
        DontThrowIfValidationFails();
        Description(builder =>
        {
            builder.ClearDefaultProduces(StatusCodes.Status400BadRequest);
        });
        Summary(summary =>
        {
            summary.Summary = "Endpoint for get chats by chat room id.";
            summary.Description =
                "This endpoint allow doctor assign the patient who they want to chat.";
            summary.Response<GetChatsByChatRoomIdHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = GetChatsByChatRoomIdResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                }
            );
        });
    }

    public override async Task<GetChatsByChatRoomIdHttpResponse> ExecuteAsync(
        GetChatsByChatRoomIdRequest req,
        CancellationToken ct
    )
    {
        var appResponse = await req.ExecuteAsync(ct: ct);

        var httpResponse = GetChatsByChatRoomIdHttpResponseMapper
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
