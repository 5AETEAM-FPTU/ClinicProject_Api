using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;

namespace Clinic.Application.Features.QueueRooms.RemoveQueueRoom;

/// <summary>
///     RemoveQueueRoom Handler.
/// </summary>
internal sealed class RemoveQueueRoomHandler
    : IFeatureHandler<RemoveQueueRoomRequest, RemoveQueueRoomResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public RemoveQueueRoomHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<RemoveQueueRoomResponse> ExecuteAsync(
        RemoveQueueRoomRequest command,
        CancellationToken ct
    )
    {
        var role = _httpContextAccessor.HttpContext.User.FindFirstValue(claimType: "role");

        if (!Equals(objA: role, objB: "user"))
        {
            return new()
            {
                StatusCode = RemoveQueueRoomResponseStatusCode.THIS_UER_IS_NOT_QUEUE_ROOM_OWNER
            };
        }

        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(claimType: "sub");

        var isQueueRoomOwner =
            await _unitOfWork.RemoveQueueRoomRepository.IsQueueRoomOwnByUserAsync(
                queueRoomId: command.QueueRoomId,
                userId: Guid.Parse(input: userId),
                cancellationToken: ct
            );

        if (!isQueueRoomOwner)
        {
            return new()
            {
                StatusCode = RemoveQueueRoomResponseStatusCode.THIS_UER_IS_NOT_QUEUE_ROOM_OWNER
            };
        }

        var dbResult = await _unitOfWork.RemoveQueueRoomRepository.DeleteQueueRoomCommandAsync(
            queueRoomId: command.QueueRoomId,
            cancellationToken: ct
        );

        if (!dbResult)
        {
            return new() { StatusCode = RemoveQueueRoomResponseStatusCode.DATABASE_OPERATION_FAIL };
        }

        return new() { StatusCode = RemoveQueueRoomResponseStatusCode.OPERATION_SUCCESS, };
    }
}
