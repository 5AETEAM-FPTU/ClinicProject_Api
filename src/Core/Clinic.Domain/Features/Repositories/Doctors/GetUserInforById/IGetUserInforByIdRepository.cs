using System;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;

namespace Clinic.Domain.Features.Repositories.Doctors.GetUserInforById;

public interface IGetUserInforByIdRepository
{
    Task<User> GetUserByUserIdQueryAsync(Guid userId, CancellationToken cancellationToken);
}
