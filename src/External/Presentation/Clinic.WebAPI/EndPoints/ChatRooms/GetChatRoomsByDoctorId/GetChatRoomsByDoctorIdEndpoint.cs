using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Features.ChatRooms.GetChatRoomsByDoctorId;
using Clinic.WebAPI.EndPoints.ChatRooms.GetChatRoomsByDoctorId.HttpResponseMapper;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.ChatRooms.GetChatRoomsByDoctorId;

/// <summary>
///     Endpoint for GetChatRoomsByDoctorId.
/// </summary>
public class GetChatRoomsByDoctorIdEndpoint
    : Endpoint<EmptyRequest, GetChatRoomsByDoctorIdHttpResponse>
{
    public override void Configure()
    {
        Get("chat-room/doctor");
        AuthSchemes(JwtBearerDefaults.AuthenticationScheme);
        DontThrowIfValidationFails();
        Description(builder =>
        {
            builder.ClearDefaultProduces(StatusCodes.Status400BadRequest);
        });
        Summary(summary =>
        {
            summary.Summary = "Endpoint for get chat rooms by doctors.";
            summary.Description = "This endpoint allow to get chat rooms by doctors.";
            summary.Response<GetChatRoomsByDoctorIdHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode =
                        GetChatRoomsByDoctorIdResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                }
            );
        });
    }

    public override async Task<GetChatRoomsByDoctorIdHttpResponse> ExecuteAsync(
        EmptyRequest req,
        CancellationToken ct
    )
    {
        var request = new GetChatRoomsByDoctorIdRequest();
        var appResponse = await request.ExecuteAsync(ct: ct);

        var httpResponse = GetChatRoomsByDoctorIdHttpResponseMapper
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
