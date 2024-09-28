using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Clinic.Domain.Features.Repositories.Enums.GetAllSpecialty;
using Clinic.Domain.Commons.Entities;
using System.Linq;

namespace Clinic.MySQL.Repositories.Enums.GetAllSpecialty;

/// <summary>
///    Implement of IGetAllSpecialty repository.
/// </summary>
internal class GetAllSpecialtyRepository : IGetAllSpecialtyRepository
{
    private readonly ClinicContext _context;
    private DbSet<Specialty> _specialtyDetails;

    public GetAllSpecialtyRepository(ClinicContext context)
    {
        _context = context;
        _specialtyDetails = _context.Set<Specialty>();
    }

    public async Task<IEnumerable<Specialty>> FindAllSpecialtyQueryAsync(CancellationToken cancellationToken)
    {
        return await _specialtyDetails
           .AsNoTracking()
           .Select(specialtyDetail => new Specialty()
           {
               Id = specialtyDetail.Id,
               Name = specialtyDetail.Name,
               Constant = specialtyDetail.Constant,
           })
           .ToListAsync(cancellationToken: cancellationToken);
    }
}
