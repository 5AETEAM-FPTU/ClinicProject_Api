using Clinic.Domain.Commons.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace Clinic.Domain.Features.Repositories.Doctors.GetAllDoctorForBooking;

/// <summary>
///     Interface for Query GetAllDoctorForBooking Repository
/// </summary>
public interface IGetAllDoctorForBookingRepository
{
    Task<IEnumerable<Doctor>> FindAllDoctorForBookingQueryAsync(
        int pageIndex,
        int pageSize,
        CancellationToken cancellationToken);
    Task<int> CountAllDoctorsQueryAsync(CancellationToken cancellationToken);
}

