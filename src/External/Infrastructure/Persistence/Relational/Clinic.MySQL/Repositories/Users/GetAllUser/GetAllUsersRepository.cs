using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Users.GetAllUser;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.MySQL.Repositories.Users.GetAllUser;

internal class GetAllUsersRepository : IGetAllUsersRepository
{

    private readonly ClinicContext _context;
    private DbSet<Domain.Commons.Entities.Patient> _patient;

    public GetAllUsersRepository(ClinicContext context)
    {
        _context = context;
        _patient = _context.Set<Domain.Commons.Entities.Patient>();
    }

    public Task<int> CountAllUserQueryAsync(CancellationToken cancellationToken)
    {
        return _patient.AsNoTracking().CountAsync(cancellationToken: cancellationToken);
    }

    public  async Task<IEnumerable<Domain.Commons.Entities.Patient>> FindUserByIdQueryAsync(int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        return await _patient
            .AsNoTracking()
            .Where(predicate: patient =>
                patient.User.RemovedAt == Application.Commons.Constance.CommonConstant.MIN_DATE_TIME
                && patient.User.RemovedBy == Application.Commons.Constance.CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
        )
            .Select(selector: patient => new Patient()
            {

                Gender = patient.Gender,
                DOB = patient.DOB,
                Description = patient.Description,
                Address = patient.Address,
                Id = patient.Id,
                User = new()
                {
                    //Id = patient.User.Id,
                    UserName = patient.User.UserName,
                    FullName = patient.User.FullName,
                    PhoneNumber = patient.User.PhoneNumber,
                    Avatar = patient.User.Avatar
                }
            })
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken: cancellationToken);
    }

}
