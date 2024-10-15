using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Features.ChatRooms.GetChatRoomsByUserId;
using Clinic.WebAPI.EndPoints.ChatRooms.GetChatRoomsByUserId.HttpResponseMapper;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.ChatRooms.GetChatRoomsByUserId;

/// <summary>
///     Endpoint for GetChatRoomsByUserId.
/// </summary>
public class GetChatRoomsByUserIdEndpoint : Endpoint<EmptyRequest, GetChatRoomsByUserIdHttpResponse>
{
    public override void Configure()
    {
        Get("chat-room/user");
        AuthSchemes(JwtBearerDefaults.AuthenticationScheme);
        DontThrowIfValidationFails();
        Description(builder =>
        {
            builder.ClearDefaultProduces(StatusCodes.Status400BadRequest);
        });
        Summary(summary =>
        {
            summary.Summary = "Endpoint for get chat rooms by patient.";
            summary.Description = "This endpoint allow to get chat rooms by patient.";
            summary.Response<GetChatRoomsByUserIdHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = GetChatRoomsByUserIdResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                }
            );
        });
    }

    public override async Task<GetChatRoomsByUserIdHttpResponse> ExecuteAsync(
        EmptyRequest req,
        CancellationToken ct
    )
    {
        var request = new GetChatRoomsByUserIdRequest();
        var appResponse = await request.ExecuteAsync(ct: ct);

        var httpResponse = GetChatRoomsByUserIdHttpResponseMapper
            .Get()
            .Resolve(statusCode: appResponse.StatusCode)
            .Invoke(arg1: request, arg2: appResponse);

        var httpResponseStatusCode = httpResponse.HttpCode;
        httpResponse.HttpCode = default;

        await SendAsync(httpResponse, httpResponseStatusCode, ct);
        httpResponse.HttpCode = httpResponseStatusCode;

        return httpResponse;
    }
}
