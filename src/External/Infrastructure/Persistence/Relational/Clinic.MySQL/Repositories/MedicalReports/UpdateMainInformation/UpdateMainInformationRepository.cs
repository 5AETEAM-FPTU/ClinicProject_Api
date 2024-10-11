using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.MedicalReports.UpdateMainInformation;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.MedicalReports.UpdateMainInformation;

internal class UpdateMainInformationRepository : IUpdateMainInformationRepository
{
    private readonly ClinicContext _context;
    private readonly DbSet<MedicalReport> _medicalReports;

    public UpdateMainInformationRepository(ClinicContext context)
    {
        _context = context;
        _medicalReports = _context.Set<MedicalReport>();
    }

    public Task<MedicalReport> GetMedicalReportsByIdQueryAsync(
        Guid medicalReportId,
        CancellationToken cancellationToken
    )
    {
        return _medicalReports
            .Where(predicate: entity => entity.Id == medicalReportId)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public async Task<bool> UpdateMainMedicalReportInformationCommandAsync(
        MedicalReport medicalReport,
        CancellationToken cancellationToken
    )
    {
        _medicalReports.Update(medicalReport);

        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }
}
