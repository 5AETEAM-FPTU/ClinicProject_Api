using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;

namespace Clinic.Domain.Features.Repositories.MedicalReports.GetMedicalReportsForStaff;

/// <summary>
///     Interface for Query GetMedicalReportsForStaff Repository
/// </summary>
public interface IGetMedicalReportsForStaffRepository
{
    Task<IEnumerable<MedicalReport>> FindAllMedicalReportByDoctorIdQueryAsync(
        string keyword,
        DateTime? lastReportDate,
        CancellationToken cancellationToken
    );
}
