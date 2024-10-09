using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;

namespace Clinic.Domain.Features.Repositories.Doctors.GetAllMedicalReport;

/// <summary>
///     Interface for Query GetAllMedicalReport Repository
/// </summary>
public interface IGetAllMedicalReportRepository
{
    Task<IEnumerable<MedicalReport>> FindAllMedicalReportByDoctorIdQueryAsync(
        string keyword,
        DateTime? lastReportDate,
        int pageSize,
        Guid doctorId,
        CancellationToken cancellationToken
    );
}
