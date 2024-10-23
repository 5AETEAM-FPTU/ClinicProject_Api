using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;

namespace Clinic.Application.Features.ChatRooms.SwitchToEndChatRoom;

/// <summary>
///     SwitchToEndChatRoom Handler.
/// </summary>
internal sealed class SwitchToEndChatRoomHandler
    : IFeatureHandler<SwitchToEndChatRoomRequest, SwitchToEndChatRoomResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SwitchToEndChatRoomHandler(
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor
    )
    {
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<SwitchToEndChatRoomResponse> ExecuteAsync(
        SwitchToEndChatRoomRequest command,
        CancellationToken ct
    )
    {
        var role = _httpContextAccessor.HttpContext.User.FindFirstValue(claimType: "role");

        if (!(Equals(objA: role, objB: "staff") || Equals(objA: role, objB: "doctor")))
        {
            return new()
            {
                StatusCode = SwitchToEndChatRoomResponseStatusCode.ROLE_IS_NOT_DOCTOR_OR_STAFF
            };
        }

        var dbResult =
            await _unitOfWork.SwitchToEndChatRoomRepository.UpdateChatRoomStatusToEndCommandAsync(
                chatRoomId: command.ChatRoomId,
                cancellationToken: ct
            );

        if (!dbResult)
        {
            return new()
            {
                StatusCode = SwitchToEndChatRoomResponseStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        return new() { StatusCode = SwitchToEndChatRoomResponseStatusCode.OPERATION_SUCCESS, };
    }
}
