using Clinic.Domain.Commons.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.Domain.Features.Repositories.Doctors.GetAllMedicalReport;

/// <summary>
///     Interface for Query GetAllMedicalReport Repository
/// </summary>
public interface IGetAllMedicalReportRepository
{
    Task<IEnumerable<MedicalReport>> FindAllMedicalReportByDoctorIdQueryAsync(
        Guid doctorId,
        CancellationToken cancellationToken
    );
}

