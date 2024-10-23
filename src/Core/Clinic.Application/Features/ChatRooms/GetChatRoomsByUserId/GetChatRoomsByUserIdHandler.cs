using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;

namespace Clinic.Application.Features.ChatRooms.GetChatRoomsByUserId;

/// <summary>
///     GetChatRoomsByUserId Handler.
/// </summary>
internal sealed class GetChatRoomsByUserIdHandler
    : IFeatureHandler<GetChatRoomsByUserIdRequest, GetChatRoomsByUserIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetChatRoomsByUserIdHandler(
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor
    )
    {
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<GetChatRoomsByUserIdResponse> ExecuteAsync(
        GetChatRoomsByUserIdRequest command,
        CancellationToken ct
    )
    {
        var role = _httpContextAccessor.HttpContext.User.FindFirstValue(claimType: "role");

        if (!Equals(objA: role, objB: "user"))
        {
            return new()
            {
                StatusCode = GetChatRoomsByUserIdResponseStatusCode.ROLE_IS_NOT_PATIENT,
            };
        }

        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(claimType: "sub");

        var chatRooms =
            await _unitOfWork.GetChatRoomsByUserIdRepository.FindAllChatRoomsByUserIdQueryAsync(
                lastTime: command.LastConversationTime,
                PageSize: command.PageSize,
                userId: Guid.Parse(input: userId),
                cancellationToken: ct
            );

        return new()
        {
            StatusCode = GetChatRoomsByUserIdResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new()
            {
                ChatRooms = chatRooms
                    .Select(selector: chatRoom => new GetChatRoomsByUserIdResponse.Body.ChatRoom()
                    {
                        ChatRoomId = chatRoom.Id,
                        DoctorId = chatRoom.Doctor.User.Id,
                        Avatar = chatRoom.Doctor?.User?.Avatar,
                        Title = chatRoom.LastMessage,
                        FullName = chatRoom.Doctor?.User?.FullName,
                        IsEndConversation = chatRoom.ExpiredTime < DateTime.Now || chatRoom.IsEnd,
                        LatestMessageTime = chatRoom.LatestTimeMessage
                    })
                    .ToList()
            }
        };
    }
}
