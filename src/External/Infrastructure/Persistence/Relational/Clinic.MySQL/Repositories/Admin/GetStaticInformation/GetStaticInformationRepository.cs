using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Admin.GetStaticInformation;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.MySQL.Repositories.Admin.GetStaticInformation;

/// <summary>
///    Implement of IGetStaticInformationRepository repository.
/// </summary>
internal class GetStaticInformationRepository : IGetStaticInformationRepository
{
    private readonly ClinicContext _context;
    private readonly DbSet<Appointment> _appointments;
    private readonly DbSet<User> _users;

    private readonly DbSet<MedicalReport> _medicalReports;
    private readonly DbSet<OnlinePayment> _onlinePayment;

    public GetStaticInformationRepository(ClinicContext context)
    {
        _context = context;
        _appointments = _context.Set<Appointment>();
        _users = _context.Set<User>();
        _medicalReports = _context.Set<MedicalReport>();
        _onlinePayment = _context.Set<OnlinePayment>();
    }

    public async Task<int> countAppointmentByDate(DateTime startDate, DateTime endDate, CancellationToken cancellationToken)
    {
        return await _appointments.Where(entity => entity.CreatedAt >= startDate && entity.CreatedAt <= endDate).CountAsync();
    }

    public Task<double> GetAverageRatingFeedback(CancellationToken cancellationToken)
    {
        return Task.FromResult(double.Parse("10"));
    }

    public Task<int> getNewUserInSystemByDate(DateTime startTime, DateTime endTime, CancellationToken cancellationToken)
    {
        return Task.FromResult(10);
    }

    public Task<double> getTotalRevenueByDate(DateTime startDate, DateTime endDate, CancellationToken cancellationToken)
    {
        return Task.FromResult(double.Parse("10"));
    }
}
