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
using static Dapper.SqlMapper;
using User = Clinic.Domain.Commons.Entities.User;

namespace Clinic.MySQL.Repositories.Admin.GetStaticInformation;

/// <summary>
///    Implement of IGetStaticInformationRepository repository.
/// </summary>
public class GetStaticInformationRepository : IGetStaticInformationRepository
{

    private readonly ClinicContext _context;
    private readonly DbSet<Appointment> _appointments;
    private readonly DbSet<User> _users;

    private readonly DbSet<MedicalReport> _medicalReports;
    private readonly DbSet<OnlinePayment> _onlinePayment;
    private readonly DbSet<Feedback> _feedback;

    public GetStaticInformationRepository(ClinicContext context)
    {
        _context = context;
        _appointments = _context.Set<Appointment>();
        _users = _context.Set<User>();
        _medicalReports = _context.Set<MedicalReport>();
        _onlinePayment = _context.Set<OnlinePayment>();
        _feedback = _context.Set<Feedback>();
    }

    public async Task<int> countAppointmentByDate(DateTime startDate, DateTime endDate, CancellationToken cancellationToken)
    {
        return await _appointments.Where(entity => entity.CreatedAt >= startDate && entity.CreatedAt <= endDate && entity.DepositPayment == true)
            .CountAsync(cancellationToken: cancellationToken);
    }

    public async Task<double> GetAverageRatingFeedback(CancellationToken cancellationToken)
    {
        var feedbackCount = await _feedback.CountAsync(cancellationToken);

        if (feedbackCount == 0)
        {
            return 0; // Trả về 0 nếu không có feedback nào tránh bị lỗi
        }

        return await _feedback.AverageAsync(entity => entity.Vote, cancellationToken);
    }

    public async Task<int> getNewUserInSystemByDate(DateTime startTime, DateTime endTime, CancellationToken cancellationToken)
    {
        var result = await (from u in _context.Users
        where u.RemovedAt == default(DateTime) && u.CreatedAt >= startTime && u.CreatedAt <= endTime
        join ur in _context.UserRoles on u.Id equals ur.UserId
        join r in _context.Roles on ur.RoleId equals r.Id
        where r.Name == "user"
        select new { u.FullName, r.Name }).ToListAsync(cancellationToken);
        return result.Count;
    }

    public async Task<int> getTotalUserByRole(string role, CancellationToken cancellationToken)
    {
        var result = await (from u in _context.Users
                            where u.RemovedAt == default(DateTime)
                            join ur in _context.UserRoles on u.Id equals ur.UserId
                            join r in _context.Roles on ur.RoleId equals r.Id
                            where r.Name == role 
                            select new { u.FullName, r.Name }).ToListAsync(cancellationToken);

        return result.Count; // Sử dụng Count thay vì ToArray().Length
    }

    public async Task<double> getTotalRevenueByDate(DateTime startDate, DateTime endDate, CancellationToken cancellationToken)
    {
        var revenueFromAppointment = await _onlinePayment
            .Where(entity => entity.CreatedAt >= startDate && entity.CreatedAt <= endDate)
            .SumAsync(entity => entity.Amount, cancellationToken);

        var revenueFromMedicalReport = await _medicalReports
            .Where(entity => entity.CreatedAt >= startDate && entity.CreatedAt <= endDate)
            .SumAsync(entity => entity.TotalPrice, cancellationToken);

        return revenueFromAppointment + revenueFromMedicalReport;
    }

    public async Task<dynamic> getMonthlyRevenue(CancellationToken cancellationToken)
    {
        var startDate = DateTime.Now.AddMonths(-11);
        var endDate = DateTime.Now;

        var onlinePayments = from payment in _onlinePayment
                             where payment.CreatedAt >= startDate && payment.CreatedAt <= endDate
                             group payment by new { payment.CreatedAt.Year, payment.CreatedAt.Month } into groupedPayments
                             select new
                             {
                                 MonthYear = $"{groupedPayments.Key.Month:00}/{groupedPayments.Key.Year}",
                                 TotalRevenue = groupedPayments.Sum(p => p.Amount)
                             };

        var medicalReports = from report in _medicalReports
                             where report.CreatedAt >= startDate && report.CreatedAt <= endDate
                             group report by new { report.CreatedAt.Year, report.CreatedAt.Month } into groupedReports
                             select new
                             {
                                 MonthYear = $"{groupedReports.Key.Month:00}/{groupedReports.Key.Year}",
                                 TotalRevenue = groupedReports.Sum(r => r.TotalPrice)
                             };
        Dictionary<string, double> result = new Dictionary<string, double>();
        var last12MonthsRevenueOnlinePayment = await onlinePayments.ToListAsync(cancellationToken);
        foreach (var item in last12MonthsRevenueOnlinePayment)
        {
            if (result.ContainsKey(item.MonthYear))
            {
                result[item.MonthYear] += item.TotalRevenue; // Cộng dồn doanh thu nếu tháng đã tồn tại
            }
            else
            {
                result[item.MonthYear] = item.TotalRevenue; // Thêm mới
            }
        }

        // Thêm doanh thu từ medicalReports vào Dictionary
        var last12MonthsRevenueMedicalReport = await medicalReports.ToListAsync(cancellationToken);
        foreach (var item in last12MonthsRevenueMedicalReport)
        {
            if (result.ContainsKey(item.MonthYear))
            {
                result[item.MonthYear] += item.TotalRevenue; // Cộng dồn doanh thu nếu tháng đã tồn tại
            }
            else
            {
                result[item.MonthYear] = item.TotalRevenue; // Thêm mới
            }
        }

        return result;
    }

    public async Task<dynamic> getMonthLyAppointment(CancellationToken cancellationToken)
    {
        var startDate = DateTime.Now.AddMonths(-11);
        var endDate = DateTime.Now;

        var countAppointment = from appointment in _appointments
                             where appointment.CreatedAt >= startDate && appointment.CreatedAt <= endDate && appointment.DepositPayment == true
                               group appointment by new { appointment.CreatedAt.Year, appointment.CreatedAt.Month } into groupedAppointments
                             select new
                             {
                                 MonthYear = $"{groupedAppointments.Key.Month:00}/{groupedAppointments.Key.Year}",
                                 Count = groupedAppointments.Count(),
                             };
        Dictionary<string, int> result = new Dictionary<string, int>();
        var counted = await countAppointment.ToListAsync(cancellationToken);
        foreach (var item in counted)
        {
            result[item.MonthYear] = item.Count;
        }

        return result;
    }
}
