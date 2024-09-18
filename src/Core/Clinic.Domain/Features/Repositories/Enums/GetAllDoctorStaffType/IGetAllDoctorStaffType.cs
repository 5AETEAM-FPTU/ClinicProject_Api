using Clinic.Domain.Commons.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.Domain.Features.Repositories.Enums.GetAllDoctorStaffType;

public interface IGetAllDoctorStaffTypeRepository
{
    Task<IEnumerable<DoctorStaffType>> FindAllDoctorStaffTypesQueryAsync(CancellationToken cancellationToken);
}


