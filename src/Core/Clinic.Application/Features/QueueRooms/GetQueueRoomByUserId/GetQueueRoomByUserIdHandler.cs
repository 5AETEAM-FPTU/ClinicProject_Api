using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;

namespace Clinic.Application.Features.QueueRooms.GetQueueRoomByUserId;

/// <summary>
///     GetQueueRoomByUserId Handler.
/// </summary>
internal sealed class GetQueueRoomByUserIdHandler
    : IFeatureHandler<GetQueueRoomByUserIdRequest, GetQueueRoomByUserIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetQueueRoomByUserIdHandler(
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor
    )
    {
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<GetQueueRoomByUserIdResponse> ExecuteAsync(
        GetQueueRoomByUserIdRequest command,
        CancellationToken ct
    )
    {
        var role = _httpContextAccessor.HttpContext.User.FindFirstValue(claimType: "role");

        if (!Equals(objA: role, objB: "user"))
        {
            return new() { StatusCode = GetQueueRoomByUserIdResponseStatusCode.FORBIDEN_ACCESS };
        }

        var userId = Guid.Parse(
            input: _httpContextAccessor.HttpContext.User.FindFirstValue(claimType: "sub")
        );

        var queueRoom =
            await _unitOfWork.GetQueueRoomByUserIdRepository.FindQueueRoomByUserIdQueryAsync(
                userId: userId,
                cancellationToken: ct
            );

        return new()
        {
            StatusCode = GetQueueRoomByUserIdResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new()
            {
                QueueRoom = new GetQueueRoomByUserIdResponse.Body.QueueRoomInformation()
                {
                    QueueRoomId = queueRoom.Id,
                    Title = queueRoom.Title,
                    Description = queueRoom.Message
                }
            }
        };
    }
}
