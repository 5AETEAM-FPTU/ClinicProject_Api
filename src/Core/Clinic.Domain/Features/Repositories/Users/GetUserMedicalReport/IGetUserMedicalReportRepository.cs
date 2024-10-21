using System;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;

namespace Clinic.Domain.Features.Repositories.Users.GetUserMedicalReport;

/// <summary>
///     Interface for Query GetUserMedicalReport Repository
/// </summary>
public interface IGetUserMedicalReportRepository
{
    Task<MedicalReport> GetMedicalReportByIdQueryAsync(
        Guid reportId,
        CancellationToken cancellationToken
    );

    Task<bool> IsAppointmentHasFeedbackQueryAsync(Guid reportId, CancellationToken cancellationToken);
}
