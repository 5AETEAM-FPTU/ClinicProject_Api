using System.Threading;
using System.Threading.Tasks;

/// <summary>
///     Interface for RemoveAppointment Repository
/// </summary>
namespace Clinic.Domain.Features.Repositories.Appointments.RemoveAppointment;

public interface IRemoveAppointmentRepository
{
    Task<bool> DeleteAppointment(CancellationToken cancellationToken = default);
}
