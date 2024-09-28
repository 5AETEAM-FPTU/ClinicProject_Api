using Clinic.Domain.Commons.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.Domain.Features.Repositories.Enums.GetAllAppointmentStatus;

/// <summary>
///     Interface for Query GetAllAppointmentStatusRepository Repository
/// </summary>
public interface IGetAllAppointmentStatusRepository
{
    Task<IEnumerable<AppointmentStatus>> FindAllAppointmentStatusQueryAsync(CancellationToken cancellationToken);
}
