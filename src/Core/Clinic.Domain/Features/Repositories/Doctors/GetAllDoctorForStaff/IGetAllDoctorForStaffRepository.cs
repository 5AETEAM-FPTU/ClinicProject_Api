using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;

namespace Clinic.Domain.Features.Repositories.Doctors.GetAllDoctorForStaff;

/// <summary>
///     Interface for Query GetAllDoctorForStaff Repository
/// </summary>
public interface IGetAllDoctorForStaffRepository
{
    Task<IEnumerable<Doctor>> FindAllDoctorsQueryAsync(
        int pageIndex,
        int pageSize,
        string keyWord,
        CancellationToken cancellationToken
    );
    Task<int> CountAllDoctorsQueryAsync(string keyWord, CancellationToken cancellationToken);
}
