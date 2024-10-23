using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Feedbacks.ViewFeedback;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.MySQL.Repositories.Feedbacks.ViewFeedback;

public class ViewFeedbackRepository : IViewFeedbackRepository
{
    private readonly ClinicContext _context;
    private readonly DbSet<Feedback> _feedbacks;
    private readonly DbSet<Appointment> _appointments;
    private readonly DbSet<User> _user;

    public ViewFeedbackRepository(ClinicContext context)
    {
        _context = context;
        _feedbacks = _context.Set<Feedback>();  
        _appointments = _context.Set<Appointment>();
        _user = _context.Set<User>();
    }

    public async Task<Appointment> GetAppointmentByIdQueryAsync(Guid reportId, CancellationToken cancellationToken)
    {
        return await _appointments
            .AsNoTracking()
            .Include(entity => entity.Schedule)
            .ThenInclude(entity => entity.Doctor)
            .Where(entity => entity.MedicalReport.Id == reportId)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<User> GetDoctorByIdQueryAsync(Guid appointmentId, CancellationToken cancellationToken)
    {
        var currentAppointment = await _appointments
            .AsNoTracking()
            .Include(entity => entity.Schedule)
            .ThenInclude(entity => entity.Doctor)
            .Where(entity => entity.Id == appointmentId)
            .FirstOrDefaultAsync(cancellationToken);

        if(currentAppointment == null)
        {
            return null;
        }

        return await _user.Where(entity => entity.Id == currentAppointment.Schedule.Doctor.UserId)
            .Select(entity => new User()
            {
                FullName = entity.FullName,
                Doctor = new()
                {
                    DoctorSpecialties = entity.Doctor.DoctorSpecialties.Select(doctorSpecialty => new DoctorSpecialty()
                    {
                        Specialty = new Specialty()
                        {
                            Id = doctorSpecialty.Specialty.Id,
                            Name = doctorSpecialty.Specialty.Name,
                            Constant = doctorSpecialty.Specialty.Constant,
                        }
                    })
                }
            })
            .FirstOrDefaultAsync(cancellationToken);
    }


    public async Task<Feedback> GetFeedBackQueryAsync(Guid appointmentId, CancellationToken cancellationToken)
    {
        return await _feedbacks
            .Where(entity => entity.AppointmentId == appointmentId)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<double> GetRatingQueryAsync(Guid doctorId, CancellationToken cancellationToken)
    {
        return await _feedbacks
            .Where(entity => entity.Appointment.Schedule.DoctorId == doctorId)
            .AverageAsync(entity => entity.Vote);
    }

    public async Task<bool> IsExistFeedback(Guid appointmentId, CancellationToken cancellationToken)
    {
        return await _appointments.AnyAsync(entity => entity.Id == appointmentId && entity.Feedback != null);
    }

}
