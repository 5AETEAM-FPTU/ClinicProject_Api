using Clinic.Domain.Commons.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.Domain.Features.Repositories.ExaminationServices.CreateService;

public interface ICreateServiceRepository
{
    Task<bool> CreateNewServiceCommandAsync(Service service, CancellationToken cancellationToken);
    Task<bool> IsExistServiceCode(string code, CancellationToken cancellationToken);
}
