using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;

namespace Clinic.Domain.Features.Repositories.ChatRooms.GetChatRoomsByDoctorId;

/// <summary>
/// Interface IGetChatRoomsByDoctorIdRepository.
/// </summary>
public interface IGetChatRoomsByDoctorIdRepository
{
    Task<IEnumerable<ChatRoom>> FindAllChatRoomsByDoctorIdQueryAsync(
        DateTime lastTime,
        int PageSize,
        Guid doctorId,
        CancellationToken cancellationToken = default
    );
}
