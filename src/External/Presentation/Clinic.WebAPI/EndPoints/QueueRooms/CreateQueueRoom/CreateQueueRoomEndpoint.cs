using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Features.QueueRooms.CreateQueueRoom;
using Clinic.WebAPI.EndPoints.QueueRooms.CreateQueueRoom.HttpResponseMapper;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.QueueRooms.CreateQueueRoom;

/// <summary>
///     Endpoint for CreateQueueRoom.
/// </summary>
public class CreateQueueRoomEndpoint : Endpoint<CreateQueueRoomRequest, CreateQueueRoomHttpResponse>
{
    public override void Configure()
    {
        Post("queue-room");
        AuthSchemes(JwtBearerDefaults.AuthenticationScheme);
        DontThrowIfValidationFails();
        Description(builder =>
        {
            builder.ClearDefaultProduces(StatusCodes.Status400BadRequest);
        });
        Summary(summary =>
        {
            summary.Summary = "Endpoint for create queue room.";
            summary.Description = "This endpoint allow patients to request a queue room in clinic.";
            summary.Response<CreateQueueRoomHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = CreateQueueRoomResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                }
            );
        });
    }

    public override async Task<CreateQueueRoomHttpResponse> ExecuteAsync(
        CreateQueueRoomRequest req,
        CancellationToken ct
    )
    {
        var appResponse = await req.ExecuteAsync(ct: ct);

        var httpResponse = CreateQueueRoomHttpResponseMapper
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
