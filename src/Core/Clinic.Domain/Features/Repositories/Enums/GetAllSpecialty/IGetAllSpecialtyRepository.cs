using Clinic.Domain.Commons.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace Clinic.Domain.Features.Repositories.Enums.GetAllSpecialty;

/// <summary>
///     Interface for Query GetAllSpecialty Repository
/// </summary>
public interface IGetAllSpecialtyRepository
{
    Task<IEnumerable<Specialty>> FindAllSpecialtyQueryAsync(CancellationToken cancellationToken);
}
