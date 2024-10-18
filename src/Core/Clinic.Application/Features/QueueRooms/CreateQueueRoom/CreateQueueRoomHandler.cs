using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Commons.ChatMessage.Notifier;
using Clinic.Application.Commons.Constance;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Clinic.Application.Features.QueueRooms.CreateQueueRoom;

/// <summary>
///     CreateQueueRoom Handler.
/// </summary>
internal sealed class CreateQueueRoomHandler
    : IFeatureHandler<CreateQueueRoomRequest, CreateQueueRoomResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly INotifierHandler _notifierHandler;
    private readonly UserManager<User> _userManager;

    public CreateQueueRoomHandler(
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor,
        INotifierHandler notifierHandler,
        UserManager<User> userManager
    )
    {
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
        _notifierHandler = notifierHandler;
        _userManager = userManager;
    }

    public async Task<CreateQueueRoomResponse> ExecuteAsync(
        CreateQueueRoomRequest command,
        CancellationToken ct
    )
    {
        var role = _httpContextAccessor.HttpContext.User.FindFirstValue(claimType: "role");
        if (!Equals(objA: role, objB: "user"))
        {
            return new() { StatusCode = CreateQueueRoomResponseStatusCode.FORBIDEN_ACCESS };
        }

        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(claimType: "sub");

        var foundUser = await _userManager.FindByIdAsync(userId: userId);

        if (Equals(objA: foundUser, objB: default))
        {
            return new() { StatusCode = CreateQueueRoomResponseStatusCode.USER_IS_NOT_FOUND };
        }

        var isSupportedUserRequest =
            await _unitOfWork.CreateQueueRoomRepository.IsPatientRequestConsultantQueryAsync(
                userId: Guid.Parse(input: userId),
                cancellationToken: ct
            );

        if (isSupportedUserRequest)
        {
            return new()
            {
                StatusCode =
                    CreateQueueRoomResponseStatusCode.USER_HAVE_BEEN_ANOTHER_REQUEST_CONSULTANT
            };
        }

        var queueRoomId = Guid.NewGuid();

        var newCreateQueueRoom = new QueueRoom()
        {
            Id = queueRoomId,
            Title = command.Title,
            Message = command.Message,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = Guid.Parse(input: userId),
            IsSuported = false,
            PatientId = Guid.Parse(input: userId),
            UpdatedAt = CommonConstant.MIN_DATE_TIME,
            UpdatedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
        };

        var dbResult = await _unitOfWork.CreateQueueRoomRepository.AddQueueRoomCommandAsync(
            queueRoom: newCreateQueueRoom,
            cancellationToken: ct
        );

        if (!dbResult)
        {
            return new() { StatusCode = CreateQueueRoomResponseStatusCode.DATABASE_OPERATION_FAIL };
        }

        await _notifierHandler.SendNotifyAsync(
            userRequestConsultant: new()
            {
                AvatarUrl = foundUser.Avatar,
                FullName = foundUser.FullName,
                Message = command.Title + ": " + command.Message,
                UserId = foundUser.Id
            }
        );

        return new()
        {
            StatusCode = CreateQueueRoomResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new() { CreateQueueRoomId = queueRoomId, }
        };
    }
}
