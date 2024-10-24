using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Features.ChatRooms.SwitchToEndChatRoom;
using Clinic.WebAPI.EndPoints.ChatRooms.SwitchToEndChatRoom.HttpResponseMapper;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.ChatRooms.SwitchToEndChatRoom;

/// <summary>
///     Endpoint for SwitchToEndChatRoom.
/// </summary>
public class SwitchToEndChatRoomEndpoint
    : Endpoint<SwitchToEndChatRoomRequest, SwitchToEndChatRoomHttpResponse>
{
    public override void Configure()
    {
        Delete("chat-room/switch-to-end/{chatRoomId}");
        AuthSchemes(JwtBearerDefaults.AuthenticationScheme);
        DontThrowIfValidationFails();
        Description(builder =>
        {
            builder.ClearDefaultProduces(StatusCodes.Status400BadRequest);
        });
        Summary(summary =>
        {
            summary.Summary = "Endpoint for switching to end chat room";
            summary.Description = "This endpoint allow doctor assign to end chat room.";
            summary.Response<SwitchToEndChatRoomHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = SwitchToEndChatRoomResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                }
            );
        });
    }

    public override async Task<SwitchToEndChatRoomHttpResponse> ExecuteAsync(
        SwitchToEndChatRoomRequest req,
        CancellationToken ct
    )
    {
        var appResponse = await req.ExecuteAsync(ct: ct);

        var httpResponse = SwitchToEndChatRoomHttpResponseMapper
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
