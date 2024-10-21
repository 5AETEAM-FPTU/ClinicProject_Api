using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;

namespace Clinic.Domain.Features.Repositories.Appointments.SwitchToCancelChatRoom;

public interface ISwitchToCancelChatRoomRepository
{
    Task<bool> SwitchToCancelChatRoom(CancellationToken cancellationToken = default);
}
