using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Clinic.Domain.Features.Repositories.Doctors.GetAllDoctorForBooking;
using Clinic.Domain.Commons.Entities;
using System.Linq;
using System;

namespace Clinic.MySQL.Repositories.Doctor.GetAllDoctorForBooking;

/// <summary>
///    Implement of IGetAllDoctorForBooking repository.
/// </summary>
internal class GetAllDoctorForBookingRepository : IGetAllDoctorForBookingRepository
{
    private readonly ClinicContext _context;
    private DbSet<Domain.Commons.Entities.Doctor> _userDetails;

    public GetAllDoctorForBookingRepository(ClinicContext context)
    {
        _context = context;
        _userDetails = _context.Set<Domain.Commons.Entities.Doctor>();
    }

    public Task<int> CountAllDoctorsQueryAsync(CancellationToken cancellationToken)
    {
        return _userDetails.AsNoTracking().CountAsync(cancellationToken: cancellationToken);
    }

    public async Task<IEnumerable<Domain.Commons.Entities.Doctor>> FindAllDoctorForBookingQueryAsync(
        int pageIndex,
        int pageSize,
        string filterName,
        Guid? specialtyId,
        Guid? genderId,
        CancellationToken cancellationToken)
    {

        var results =  _userDetails
            .AsNoTracking()
            .AsQueryable();

                if (specialtyId != default) 
                {
                    results = results.Where(entity => entity.DoctorSpecialties.Any(doctorSpecialty => doctorSpecialty.SpecialtyID == specialtyId));
                }

                if (genderId != default) 
                {
                   results = results.Where(entity => entity.User.GenderId == genderId);
                }


       results = results.Where(entity => entity.User.FullName.Contains(filterName));


        return await results
            .Where(doctor => doctor.Schedules != null && doctor.Schedules.Any(schedule => schedule.StartDate > DateTime.Now))
            .Select(selector: doctor => new Domain.Commons.Entities.Doctor()
            {
                UserId = doctor.UserId,
                DOB = doctor.DOB,
                Description = doctor.Description,
                Position = new()
                {
                    Name = doctor.Position.Name,
                    Constant = doctor.Position.Constant,
                    Id = doctor.Position.Id,
                },
                DoctorSpecialties = doctor.DoctorSpecialties.Select(doctorSpecialty => new DoctorSpecialty()
                {
                    Specialty = new()
                    {
                        Constant = doctorSpecialty.Specialty.Constant,
                        Name = doctorSpecialty.Specialty.Name,
                        Id = doctorSpecialty.Specialty.Id,
                    },
                })
                .ToList(),
                Schedules = doctor.Schedules.Select(doctorSchedule => new Schedule()
                {
                    Appointment = new Appointment()
                    {
                        Feedback = new Feedback()
                        {
                            Vote = doctorSchedule.Appointment.Feedback != null ? doctorSchedule.Appointment.Feedback.Vote : 0,
                        }
                    }
                }),
                Address = doctor.Address,
                Achievement = doctor.Achievement,
                User = new User()
                {
                    UserName = doctor.User.UserName,
                    FullName = doctor.User.FullName,
                    PhoneNumber = doctor.User.PhoneNumber,
                    Avatar = doctor.User.Avatar,
                    Gender = new()
                    {
                        Name = doctor.User.Gender.Name,
                        Constant = doctor.User.Gender.Constant,
                        Id = doctor.User.Gender.Id,
                    },
                }

            })
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken: cancellationToken);
    }
}
