using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Features.ChatRooms.AssignChatRoom;
using Clinic.WebAPI.EndPoints.ChatRooms.AssignChatRoom.HttpResponseMapper;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.ChatRooms.AssignChatRoom;

/// <summary>
///     Endpoint for AssignChatRoom.
/// </summary>
public class AssignChatRoomEndpoint : Endpoint<AssignChatRoomRequest, AssignChatRoomHttpResponse>
{
    public override void Configure()
    {
        Post("chat-room");
        AuthSchemes(JwtBearerDefaults.AuthenticationScheme);
        DontThrowIfValidationFails();
        Description(builder =>
        {
            builder.ClearDefaultProduces(StatusCodes.Status400BadRequest);
        });
        Summary(summary =>
        {
            summary.Summary = "Endpoint for create chat room.";
            summary.Description =
                "This endpoint allow doctor assign the patient who they want to consultant in clinic.";
            summary.Response<AssignChatRoomHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = AssignChatRoomResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                }
            );
        });
    }

    public override async Task<AssignChatRoomHttpResponse> ExecuteAsync(
        AssignChatRoomRequest req,
        CancellationToken ct
    )
    {
        var appResponse = await req.ExecuteAsync(ct: ct);

        var httpResponse = AssignChatRoomHttpResponseMapper
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
