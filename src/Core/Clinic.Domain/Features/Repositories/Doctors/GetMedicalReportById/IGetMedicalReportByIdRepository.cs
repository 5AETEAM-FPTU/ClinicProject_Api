using Clinic.Domain.Commons.Entities;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Clinic.Domain.Features.Repositories.Doctors.GetMedicalReportById;

/// <summary>
///     Interface for Query GetMedicalReportById Repository
/// </summary>
public interface IGetMedicalReportByIdRepository
{
    Task<MedicalReport> GetMedicalReportByIdQueryAsync(Guid reportId, CancellationToken cancellationToken);
}
