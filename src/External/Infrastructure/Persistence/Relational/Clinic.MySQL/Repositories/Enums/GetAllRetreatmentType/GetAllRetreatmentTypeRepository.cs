﻿using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Clinic.Domain.Features.Repositories.Enums.GetAllRetreatmentType;
using Clinic.Domain.Commons.Entities;
using System.Linq;

namespace Clinic.MySQL.Repositories.Enums.GetAllRetreatmentType;

/// <summary>
///    Implement of IGetAllRetreatmentType repository.
/// </summary>
internal class GetAllRetreatmentTypeRepository : IGetAllRetreatmentTypeRepository
{
    private readonly ClinicContext _context;
    private DbSet<RetreatmentType> _retreatmentDetails;

    public GetAllRetreatmentTypeRepository(ClinicContext context)
    {
        _context = context;
        _retreatmentDetails = _context.Set<RetreatmentType>();
    }

    public async Task<IEnumerable<RetreatmentType>> FindAllRetreatmentTypeQueryAsync(CancellationToken cancellationToken)
    {
        return await _retreatmentDetails
           .AsNoTracking()
           .Select(retreatmentDetail => new RetreatmentType()
           {
               Id = retreatmentDetail.Id,
               Name = retreatmentDetail.Name,
               Constant = retreatmentDetail.Constant,
           })
           .ToListAsync(cancellationToken: cancellationToken);
    }
}