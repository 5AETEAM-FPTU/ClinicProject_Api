using System;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;

namespace Clinic.Domain.Features.Repositories.Doctors.AddDoctor;

public interface IAddDoctorRepository
{
    Task<bool> IsDoctorStaffTypeFoundByIdQueryAsync(
        Guid doctorStaffId,
        CancellationToken cancellationToken
    );

    Task<bool> CreateDoctorCommandAsync(
        User doctor,
        string userPassword,
        string roleName,
        CancellationToken cancellationToken
    );
}
