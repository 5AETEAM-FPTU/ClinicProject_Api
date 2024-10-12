using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.MedicalReports.UpdatePatientInformation;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.MedicalReports.UpdatePatientInformation;

public class UpdatePatientInformationRepository : IUpdatePatientInformationRepository
{
    private readonly ClinicContext _clinicContext;
    private readonly DbSet<PatientInformation> _patientInformation;

    public UpdatePatientInformationRepository(ClinicContext clinicContext)
    {
        _clinicContext = clinicContext;
        _patientInformation = _clinicContext.Set<PatientInformation>();
    }

    public Task<PatientInformation> FindPatientInformationByIdQueryAsync(
        Guid patientInformationId,
        CancellationToken ct
    )
    {
        return _patientInformation.FirstOrDefaultAsync(
            entity => entity.Id == patientInformationId,
            ct
        );
    }

    public async Task<bool> UpdatePatientInformationCommandAsync(
        PatientInformation patientInformation,
        CancellationToken ct
    )
    {
        _patientInformation.Update(patientInformation);
        return await _clinicContext.SaveChangesAsync(ct) > 0;
    }
}
