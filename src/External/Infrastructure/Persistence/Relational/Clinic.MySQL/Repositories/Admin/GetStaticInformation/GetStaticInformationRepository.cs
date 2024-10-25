using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Admin.GetStaticInformation;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
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
    private readonly IDbContextFactory<ClinicContext> _contextFactory;
    private readonly DbSet<Appointment> _appointments;
    private readonly DbSet<User> _users;

    private readonly DbSet<MedicalReport> _medicalReports;
    private readonly DbSet<OnlinePayment> _onlinePayment;
    private readonly DbSet<Feedback> _feedback;

    public GetStaticInformationRepository(ClinicContext context, IDbContextFactory<ClinicContext> contextFactory)
    {
        _context = context;
        _contextFactory = contextFactory;
        _appointments = _context.Set<Appointment>();
        _users = _context.Set<User>();
        _medicalReports = _context.Set<MedicalReport>();
        _onlinePayment = _context.Set<OnlinePayment>();
        _feedback = _context.Set<Feedback>();
    }

    public async Task<int> countAppointmentByDate(DateTime startDate, DateTime endDate, CancellationToken cancellationToken)
    {
        var task1 = Task.Run(async () =>
        {
            using (var context1 = _contextFactory.CreateDbContext())
            {
                return await context1.Set<Appointment>()
                .Where(entity => entity.CreatedAt >= startDate && entity.CreatedAt <= endDate && entity.DepositPayment == true)
                .CountAsync(cancellationToken: cancellationToken);
            };
        });
        
        return await task1;
    }

    public async Task<double> GetAverageRatingFeedback(CancellationToken cancellationToken)
    {
        var task1 = Task.Run(async () =>
        {
            using (var context1 = _contextFactory.CreateDbContext())
            {
                var feedbackCount = await context1.Set<Feedback>().CountAsync(cancellationToken);
                if (feedbackCount == 0)
                {
                    return 0; // Trả về 0 nếu không có feedback nào tránh bị lỗi
                }
                return await context1.Set<Feedback>().AverageAsync(entity => entity.Vote, cancellationToken);
            };
        });

        return await task1;
    }

    public async Task<int> getNewUserInSystemByDate(DateTime startTime, DateTime endTime, CancellationToken cancellationToken)
    {
        var task1 = Task.Run(async () =>
        {
            using (var context1 = _contextFactory.CreateDbContext())
            {
                var result = await (from u in context1.Set<User>()
                                    where u.RemovedAt == default(DateTime) && u.CreatedAt >= startTime && u.CreatedAt <= endTime
                                    join ur in context1.UserRoles on u.Id equals ur.UserId
                                    join r in context1.Roles on ur.RoleId equals r.Id
                                    where r.Name == "user"
                                    select new { u.FullName, r.Name }).ToListAsync(cancellationToken);
                return result.Count;
            };
        });
        return await task1;
    }

    public async Task<int> getTotalUserByRole(string role, CancellationToken cancellationToken)
    {
        var task1 = Task.Run(async () =>
        {
            using (var context1 = _contextFactory.CreateDbContext())
            {
                var result = await (from u in context1.Users
                                    where u.RemovedAt == default(DateTime)
                                    join ur in context1.UserRoles on u.Id equals ur.UserId
                                    join r in context1.Roles on ur.RoleId equals r.Id
                                    where r.Name == role
                                    select new { u.FullName, r.Name }).ToListAsync(cancellationToken);
                return result.Count;
            };
        });
        return await task1;
    }

    public async Task<double> getTotalRevenueByDate(DateTime startDate, DateTime endDate, CancellationToken cancellationToken)
    {
        var task1 = Task.Run(async () =>
        {
            using (var context1 = _contextFactory.CreateDbContext())
            {
                return await context1.Set<OnlinePayment>()
                .Where(entity => entity.CreatedAt >= startDate && entity.CreatedAt <= endDate)
                .SumAsync(entity => entity.Amount, cancellationToken);
            };
        });

        var task2 = Task.Run(async () =>
        {
            using (var context1 = _contextFactory.CreateDbContext())
            {
                return await context1.Set<MedicalReport>()
                .Where(entity => entity.CreatedAt >= startDate && entity.CreatedAt <= endDate)
                .SumAsync(entity => entity.TotalPrice, cancellationToken);
            };
        });


        var result = await Task.WhenAll(task1, task2);
        

        return result[0] + result[1];
    }

    public async Task<dynamic> getMonthlyRevenue(CancellationToken cancellationToken)
    {
        var startDate = DateTime.Now.AddMonths(-11);
        var endDate = DateTime.Now;

        var task1 = Task.Run(async () =>
        {
            using (var context1 = _contextFactory.CreateDbContext())
            {
                var onlinePayments = await (from payment in context1.Set<OnlinePayment>()
                                     where payment.CreatedAt >= startDate && payment.CreatedAt <= endDate
                                     group payment by new { payment.CreatedAt.Year, payment.CreatedAt.Month } into groupedPayments
                                     select new
                                     {
                                         MonthYear = $"{groupedPayments.Key.Month:00}/{groupedPayments.Key.Year}",
                                         TotalRevenue = groupedPayments.Sum(p => p.Amount)
                                     }).ToListAsync(cancellationToken);
                return onlinePayments;
            };
        });

        var task2 = Task.Run(async () =>
        {
            using (var context1 = _contextFactory.CreateDbContext())
            {
                var medicalReports = await (from report in context1.Set<MedicalReport>()
                                            where report.CreatedAt >= startDate && report.CreatedAt <= endDate
                                            group report by new { report.CreatedAt.Year, report.CreatedAt.Month } into groupedReports
                                            select new
                                            {
                                                MonthYear = $"{groupedReports.Key.Month:00}/{groupedReports.Key.Year}",
                                                TotalRevenue = groupedReports.Sum(r => r.TotalPrice)
                                            }).ToListAsync(cancellationToken);
                return medicalReports;
            };
        });
        var resultTasks = await Task.WhenAll(task1,  task2);
        Dictionary<string, double> result = new Dictionary<string, double>();
        var last12MonthsRevenueOnlinePayment = resultTasks[0];
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
        var last12MonthsRevenueMedicalReport = resultTasks[1];
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
        var task = Task.Run(async () =>
        {
            using (var context1 = _contextFactory.CreateDbContext())
            {
                var countAppointment = await (from appointment in context1.Set<Appointment>()
                                              where appointment.CreatedAt >= startDate && appointment.CreatedAt <= endDate && appointment.DepositPayment == true
                                              group appointment by new { appointment.CreatedAt.Year, appointment.CreatedAt.Month } into groupedAppointments
                                              select new
                                              {
                                                  MonthYear = $"{groupedAppointments.Key.Month:00}/{groupedAppointments.Key.Year}",
                                                  Count = groupedAppointments.Count(),
                                              }).ToListAsync(cancellationToken);
                return countAppointment;
            };
        });
        Dictionary<string, int> result = new Dictionary<string, int>();
        var counted = await task;
        foreach (var item in counted)
        {
            result[item.MonthYear] = item.Count;
        }

        return result;
    }

    public async Task<dynamic> getFastOverviewInformation(CancellationToken ct)
    {
        var startOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1); // Ngày đầu tiên của tháng hiện tại
        var lastDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)).AddDays(1).AddTicks(-1);
        var startOfYear = new DateTime(DateTime.Now.Year, 1, 1);
        var endOfYear = new DateTime(DateTime.Now.Year + 1, 1, 1).AddTicks(-1);

        // Create tasks for asynchronous operations
        var revenueInCurrentMonthTask = getTotalRevenueByDate(startOfMonth, lastDayOfMonth, ct);
        var revenueInCurrentYearTask = getTotalRevenueByDate(startOfYear, endOfYear, ct);
        var newUserInCurrentMonthTask = getNewUserInSystemByDate(startOfMonth, lastDayOfMonth, ct);
        var totalDoctorTask = getTotalUserByRole("doctor", ct);
        var totalStaffTask = getTotalUserByRole("staff", ct);
        var totalPatientTask = getTotalUserByRole("user", ct);
        var averageFeedbackTask = GetAverageRatingFeedback(ct);
        var appointmentInCurrentMonthTask = countAppointmentByDate(startOfMonth, lastDayOfMonth, ct);
        var monthlyRevenueTask = getMonthlyRevenue(ct);
        var monthlyAppointmentTask = getMonthLyAppointment(ct);

        // Await all tasks to complete in parallel
        await Task.WhenAll(
            revenueInCurrentMonthTask,
            revenueInCurrentYearTask,
            newUserInCurrentMonthTask,
            totalDoctorTask,
            totalStaffTask,
            totalPatientTask,
            averageFeedbackTask,
            appointmentInCurrentMonthTask,
            monthlyRevenueTask,
            monthlyAppointmentTask
        );

        return new
        {
            monthlyRevenue = await monthlyRevenueTask,
            revenueInCurrentMonth = await revenueInCurrentMonthTask,
            revenueInCurrentYear = await revenueInCurrentYearTask,
            totalDoctor = await totalDoctorTask,
            totalStaff = await totalStaffTask,
            totalPatient = await totalPatientTask,
            newUserInCurrentMonth = await newUserInCurrentMonthTask,
            averageFeedback = await averageFeedbackTask,
            appointmentInCurrentMonth = await appointmentInCurrentMonthTask,
            monthlyAppointment = await monthlyAppointmentTask
        };
    }

}
