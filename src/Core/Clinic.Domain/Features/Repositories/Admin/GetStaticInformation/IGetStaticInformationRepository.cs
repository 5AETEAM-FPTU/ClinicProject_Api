using Clinic.Domain.Commons.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.Domain.Features.Repositories.Admin.GetStaticInformation;

public interface IGetStaticInformationRepository
{
    Task<int> getTotalUserByRole(string role, CancellationToken cancellationToken);
    Task<double> GetAverageRatingFeedback(
        CancellationToken cancellationToken
    );

    Task<int> countAppointmentByDate(DateTime startDate, DateTime endDate, CancellationToken cancellationToken);

    Task<double> getTotalRevenueByDate(DateTime startDate, DateTime endDate, CancellationToken cancellationToken);

    Task<dynamic> getMonthlyRevenue(int year, CancellationToken cancellationToken);

    Task<dynamic> getMonthLyAppointment(int year,CancellationToken cancellationToken);
    Task<int> getNewUserInSystemByDate(DateTime startTime, DateTime endTime, CancellationToken cancellationToken);
    Task<dynamic> getFastOverviewInformation(int year, CancellationToken cancellationToken);
}
