using System;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.Domain.Features.Repositories.Admin.GetStaticInformation;

public interface IGetStaticInformationRepository
{
    Task<double> GetAverageRatingFeedback(
        CancellationToken cancellationToken
    );

    Task<int> countAppointmentByDate(DateTime startDate, DateTime endDate, CancellationToken cancellationToken);

    Task<double> getTotalRevenueByDate(DateTime startDate, DateTime endDate, CancellationToken cancellationToken);

    Task<int> getNewUserInSystemByDate(DateTime startTime, DateTime endTime, CancellationToken cancellationToken);
}
