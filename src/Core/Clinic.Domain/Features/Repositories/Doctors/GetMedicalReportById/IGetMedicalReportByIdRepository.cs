using System;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;

namespace Clinic.Domain.Features.Repositories.Doctors.GetMedicalReportById;

/// <summary>
///     Interface for Query GetMedicalReportById Repository
/// </summary>
public interface IGetMedicalReportByIdRepository
{
    Task<bool> IsFeedbackExistByAppointmentIdQueryAynsc(
        Guid appointmentId,
        CancellationToken cancellationToken
    );

    Task<MedicalReport> GetMedicalReportByIdQueryAsync(
        Guid reportId,
        CancellationToken cancellationToken
    );
}
