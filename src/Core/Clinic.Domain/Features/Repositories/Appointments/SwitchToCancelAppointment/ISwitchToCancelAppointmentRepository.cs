using System.Threading;
using System.Threading.Tasks;

/// <summary>
///     Interface for SwitchToCancelChatRoomRepository
/// </summary>
namespace Clinic.Domain.Features.Repositories.Appointments.SwitchToCancelAppointment;

public interface ISwitchToCancelAppointmentRepository
{
    Task<bool> SwitchToCancelAppointment(CancellationToken cancellationToken = default);
}
