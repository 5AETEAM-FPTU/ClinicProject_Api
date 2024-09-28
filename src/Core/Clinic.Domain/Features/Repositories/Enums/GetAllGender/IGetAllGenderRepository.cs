using Clinic.Domain.Commons.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace Clinic.Domain.Features.Repositories.Enums.GetAllGender;

/// <summary>
///     Interface for Query GetAllGender Repository
/// </summary>
public interface IGetAllGenderRepository
{
    Task<IEnumerable<Gender>> FindAllGenderQueryAsync(CancellationToken cancellationToken);
}