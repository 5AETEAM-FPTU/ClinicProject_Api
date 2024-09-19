using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Enums.GetAllDoctorStaffType;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.MySQL.Repositories.Enums.GetAllDoctorStaffType;

internal class GetAllDoctorStaffTypeRepository : IGetAllDoctorStaffTypeRepository
{
    private readonly ClinicContext _context;
    private DbSet<DoctorStaffType> _doctorStaffTypes;

    public GetAllDoctorStaffTypeRepository(ClinicContext context)
    {
        _context = context;
        _doctorStaffTypes = _context.Set<DoctorStaffType>();
    }

    public async Task<IEnumerable<DoctorStaffType>> FindAllDoctorStaffTypesQueryAsync(CancellationToken cancellationToken)
    {
        return  await _doctorStaffTypes
           .AsNoTracking()
           .Where(predicate: doctorStaffType =>
               doctorStaffType.RemovedAt == Application.Commons.Constance.CommonConstant.MIN_DATE_TIME
               && doctorStaffType.RemovedBy
                   == Application.Commons.Constance.CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
           )
           .Select(doctorStaffType => new DoctorStaffType()
           {
               Id = doctorStaffType.Id,
               TypeName = doctorStaffType.TypeName,
               Constant = doctorStaffType.Constant,
           })
           .ToListAsync(cancellationToken: cancellationToken);
    }
}
