using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Commons.Constance;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;

namespace Clinic.Application.Features.ChatRooms.AssignChatRoom;

/// <summary>
///     AssignChatRoom Handler.
/// </summary>
internal sealed class AssignChatRoomHandler
    : IFeatureHandler<AssignChatRoomRequest, AssignChatRoomResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AssignChatRoomHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<AssignChatRoomResponse> ExecuteAsync(
        AssignChatRoomRequest command,
        CancellationToken ct
    )
    {
        var role = _httpContextAccessor.HttpContext.User.FindFirstValue(claimType: "role");

        if (!(Equals(objA: role, objB: "staff") || Equals(objA: role, objB: "doctor")))
        {
            return new() { StatusCode = AssignChatRoomResponseStatusCode.FORBIDEN_ACCESS };
        }

        var doctorId = _httpContextAccessor.HttpContext.User.FindFirstValue(claimType: "sub");

        var isPatientFound =
            await _unitOfWork.AssignChatRoomRepository.IsPatientFoundByIdQueryAsync(
                patientId: command.PatientId,
                cancellationToken: ct
            );

        if (!isPatientFound)
        {
            return new() { StatusCode = AssignChatRoomResponseStatusCode.PATIENT_ID_NOT_FOUND };
        }

        var chatRoomId = Guid.NewGuid();

        var newChatRoom = new ChatRoom()
        {
            Id = chatRoomId,
            DoctorId = Guid.Parse(input: doctorId),
            CreatedAt = CommonConstant.DATE_NOW_UTC,
            CreatedBy = Guid.Parse(input: doctorId),
            LastMessage = command.InitialMessage,
            UpdatedAt = CommonConstant.DATE_NOW_UTC,
            UpdatedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
            PatientId = command.PatientId,
            ChatContents = new List<ChatContent>()
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    TextContent = command.InitialMessage,
                    CreatedAt = CommonConstant.DATE_NOW_UTC,
                    CreatedBy = CommonConstant.SYSTEM_GUID,
                    UpdatedAt = CommonConstant.MIN_DATE_TIME,
                    UpdatedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
                    RemovedAt = CommonConstant.MIN_DATE_TIME,
                    RemovedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
                    IsRead = false,
                    SenderId = command.PatientId,
                }
            }
        };

        var dbResult = await _unitOfWork.AssignChatRoomRepository.AddChatRoomCommandAsync(
            chatRoom: newChatRoom,
            cancellationToken: ct
        );

        if (!dbResult)
        {
            return new() { StatusCode = AssignChatRoomResponseStatusCode.DATABASE_OPERATION_FAIL };
        }

        return new()
        {
            StatusCode = AssignChatRoomResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new() { AssignChatRoomId = chatRoomId, }
        };
    }
}
