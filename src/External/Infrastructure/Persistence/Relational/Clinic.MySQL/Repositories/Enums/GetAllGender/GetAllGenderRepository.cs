using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Clinic.Domain.Features.Repositories.Enums.GetAllGender;
using Clinic.Domain.Commons.Entities;
using System.Linq;

namespace Clinic.MySQL.Repositories.Enums.GetAllGender;

/// <summary>
///    Implement of IGetAllGender repository.
/// </summary>
internal class GetAllGenderRepository : IGetAllGenderRepository
{
    private readonly ClinicContext _context;
    private DbSet<Gender> _genderDetails;

    public GetAllGenderRepository(ClinicContext context)
    {
        _context = context;
        _genderDetails = _context.Set<Gender>();
    }

    public async Task<IEnumerable<Gender>> FindAllGenderQueryAsync(CancellationToken cancellationToken)
    {
        return await _genderDetails
           .AsNoTracking()
           .Select(genderDetail => new Gender()
           {
               Id = genderDetail.Id,
               Name = genderDetail.Name,
               Constant = genderDetail.Constant,
           })
           .ToListAsync(cancellationToken: cancellationToken);
    }
}
