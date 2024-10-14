using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Features.QueueRooms.GetAllQueueRooms;
using Clinic.WebAPI.EndPoints.QueueRooms.GetAllQueueRooms.HttpResponseMapper;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.QueueRooms.GetAllQueueRooms;

/// <summary>
///     Endpoint for GetAllQueueRooms.
/// </summary>
public class GetAllQueueRoomsEndpoint
    : Endpoint<GetAllQueueRoomsRequest, GetAllQueueRoomsHttpResponse>
{
    public override void Configure()
    {
        Get("queue-room/all");
        AuthSchemes(JwtBearerDefaults.AuthenticationScheme);
        DontThrowIfValidationFails();
        Description(builder =>
        {
            builder.ClearDefaultProduces(StatusCodes.Status400BadRequest);
        });
        Summary(summary =>
        {
            summary.Summary = "Endpoint for get all queue rooms in clinic.";
            summary.Description = "This endpoint allow doctor to get all queue rooms.";
            summary.Response<GetAllQueueRoomsHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = GetAllQueueRoomsResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                }
            );
        });
    }

    public override async Task<GetAllQueueRoomsHttpResponse> ExecuteAsync(
        GetAllQueueRoomsRequest req,
        CancellationToken ct
    )
    {
        var appResponse = await req.ExecuteAsync(ct: ct);

        var httpResponse = GetAllQueueRoomsHttpResponseMapper
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
