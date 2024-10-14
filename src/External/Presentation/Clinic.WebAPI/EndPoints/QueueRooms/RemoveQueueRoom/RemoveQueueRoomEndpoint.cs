using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Features.QueueRooms.RemoveQueueRoom;
using Clinic.WebAPI.EndPoints.QueueRooms.RemoveQueueRoom.HttpResponseMapper;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.QueueRooms.RemoveQueueRoom;

/// <summary>
///     Endpoint for RemoveQueueRoom.
/// </summary>
public class RemoveQueueRoomEndpoint : Endpoint<RemoveQueueRoomRequest, RemoveQueueRoomHttpResponse>
{
    public override void Configure()
    {
        Delete("queue-room/{queueRoomId}");
        AuthSchemes(JwtBearerDefaults.AuthenticationScheme);
        DontThrowIfValidationFails();
        Description(builder =>
        {
            builder.ClearDefaultProduces(StatusCodes.Status400BadRequest);
        });
        Summary(summary =>
        {
            summary.Summary = "Endpoint for remove queue room.";
            summary.Description = "This endpoint allow patients to remove queue room.";
            summary.Response<RemoveQueueRoomHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = RemoveQueueRoomResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                }
            );
        });
    }

    public override async Task<RemoveQueueRoomHttpResponse> ExecuteAsync(
        RemoveQueueRoomRequest req,
        CancellationToken ct
    )
    {
        var appResponse = await req.ExecuteAsync(ct: ct);

        var httpResponse = RemoveQueueRoomHttpResponseMapper
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
