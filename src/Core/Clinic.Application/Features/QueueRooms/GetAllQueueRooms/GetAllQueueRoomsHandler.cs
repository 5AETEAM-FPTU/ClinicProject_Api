using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;

namespace Clinic.Application.Features.QueueRooms.GetAllQueueRooms;

/// <summary>
///     GetAllQueueRooms Handler.
/// </summary>
internal sealed class GetAllQueueRoomsHandler
    : IFeatureHandler<GetAllQueueRoomsRequest, GetAllQueueRoomsResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetAllQueueRoomsHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<GetAllQueueRoomsResponse> ExecuteAsync(
        GetAllQueueRoomsRequest command,
        CancellationToken ct
    )
    {
        var role = _httpContextAccessor.HttpContext.User.FindFirstValue(claimType: "role");

        if (!(Equals(objA: role, objB: "staff") || Equals(objA: role, objB: "doctor")))
        {
            return new() { StatusCode = GetAllQueueRoomsResponseStatusCode.FORBIDEN_ACCESS };
        }

        var queueRooms = await _unitOfWork.GetAllQueueRoomsRepository.FindAllQueueRoomsQueryAsync(
            pageIndex: command.PageIndex,
            pageSize: command.PageSize,
            cancellationToken: ct
        );

        var countQueueRooms =
            await _unitOfWork.GetAllQueueRoomsRepository.CountQueueRoomsQueryAsync(
                cancellationToken: ct
            );

        return new()
        {
            StatusCode = GetAllQueueRoomsResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new()
            {
                PatientQueues = new()
                {
                    Contents = queueRooms
                        .Select(queueRoom => new GetAllQueueRoomsResponse.Body.PatientQueue()
                        {
                            PatientId = queueRoom.Patient.User.Id,
                            PatientName = queueRoom.Patient.User.FullName,
                            QueueRoomId = queueRoom.Id,
                            Message = queueRoom.Title + ": " + queueRoom.Message,
                            PatientAvatar = queueRoom.Patient.User.Avatar
                        })
                        .ToList(),
                    PageIndex = command.PageIndex,
                    PageSize = command.PageSize,
                    TotalPages = (int)Math.Ceiling((double)countQueueRooms / command.PageSize),
                }
            }
        };
    }
}
