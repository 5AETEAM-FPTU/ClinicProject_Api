using Clinic.Domain.Commons.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.Domain.Features.Repositories.ExaminationServices.GetDetailService;

public interface IGetDetailServiceRepository
{
    Task<Service> GetDetailServiceByIdQueryAsync(Guid Id, CancellationToken cancellationToken);
    Task<bool> IsServiceExisted(Guid serviceId, CancellationToken cancellationToken);
}
