using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;

namespace Clinic.Application.Features.ChatRooms.GetChatRoomsByDoctorId;

/// <summary>
///     GetChatRoomsByDoctorId Handler.
/// </summary>
internal sealed class GetChatRoomsByDoctorIdHandler
    : IFeatureHandler<GetChatRoomsByDoctorIdRequest, GetChatRoomsByDoctorIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetChatRoomsByDoctorIdHandler(
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor
    )
    {
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<GetChatRoomsByDoctorIdResponse> ExecuteAsync(
        GetChatRoomsByDoctorIdRequest command,
        CancellationToken ct
    )
    {
        var role = _httpContextAccessor.HttpContext.User.FindFirstValue(claimType: "role");

        if (!Equals(objA: role, objB: "doctor"))
        {
            return new()
            {
                StatusCode = GetChatRoomsByDoctorIdResponseStatusCode.ROLE_IS_NOT_DOCTOR,
            };
        }

        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(claimType: "sub");

        var chatRooms =
            await _unitOfWork.GetChatRoomsByDoctorIdRepository.FindAllChatRoomsByDoctorIdQueryAsync(
                doctorId: Guid.Parse(input: userId),
                cancellationToken: ct
            );

        return new()
        {
            StatusCode = GetChatRoomsByDoctorIdResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new()
            {
                ChatRooms = chatRooms
                    .Select(selector: chatRoom => new GetChatRoomsByDoctorIdResponse.Body.ChatRoom()
                    {
                        UserId = chatRoom.Patient.User.Id,
                        ChatRoomId = chatRoom.Id,
                        Avatar = chatRoom.Patient?.User?.Avatar,
                        Title = chatRoom.LastMessage,
                        FullName = chatRoom.Patient?.User?.FullName,
                        IsEndConversation = chatRoom.ExpiredTime < DateTime.Now
                    })
                    .ToList()
            }
        };
    }
}
