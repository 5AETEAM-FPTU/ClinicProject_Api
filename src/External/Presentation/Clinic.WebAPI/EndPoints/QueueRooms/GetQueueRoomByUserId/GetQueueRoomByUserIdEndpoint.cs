using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Features.QueueRooms.GetQueueRoomByUserId;
using Clinic.WebAPI.EndPoints.QueueRooms.GetQueueRoomByUserId.HttpResponseMapper;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.QueueRooms.GetQueueRoomByUserId;

/// <summary>
///     Endpoint for GetQueueRoomByUserId.
/// </summary>
public class GetQueueRoomByUserIdEndpoint : Endpoint<EmptyRequest, GetQueueRoomByUserIdHttpResponse>
{
    public override void Configure()
    {
        Get("queue-room/user-id");
        AuthSchemes(JwtBearerDefaults.AuthenticationScheme);
        DontThrowIfValidationFails();
        Description(builder =>
        {
            builder.ClearDefaultProduces(StatusCodes.Status400BadRequest);
        });
        Summary(summary =>
        {
            summary.Summary = "Endpoint for get queue room by user id.";
            summary.Description = "This endpoint allow user for getting queue room information.";
            summary.Response<GetQueueRoomByUserIdHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = GetQueueRoomByUserIdResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                }
            );
        });
    }

    public override async Task<GetQueueRoomByUserIdHttpResponse> ExecuteAsync(
        EmptyRequest req,
        CancellationToken ct
    )
    {
        var request = new GetQueueRoomByUserIdRequest();

        var appResponse = await request.ExecuteAsync(ct: ct);

        var httpResponse = GetQueueRoomByUserIdHttpResponseMapper
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
