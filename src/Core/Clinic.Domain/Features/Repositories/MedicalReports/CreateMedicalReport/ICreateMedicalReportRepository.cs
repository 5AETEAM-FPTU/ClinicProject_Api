using System;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;

namespace Clinic.Domain.Features.Repositories.MedicalReports.CreateMedicalReport;

/// <summary>
///     Interface for CreateMedicalReport.
/// </summary>
public interface ICreateMedicalReportRepository
{
    Task<User> FindPatientByIdQueryAsync(Guid patientId, CancellationToken cancellationToken);

    Task<bool> IsAppointmentFoundByIdQueryAsync(
        Guid appointmentId,
        CancellationToken cancellationToken
    );

    Task<bool> IsAppointmentReportedQueryAsync(
        Guid appointmentId,
        CancellationToken cancellationToken
    );

    Task<bool> CreateMedicalReportCommandAsync(
        MedicalReport newMedicalReport,
        CancellationToken cancellationToken
    );
}
