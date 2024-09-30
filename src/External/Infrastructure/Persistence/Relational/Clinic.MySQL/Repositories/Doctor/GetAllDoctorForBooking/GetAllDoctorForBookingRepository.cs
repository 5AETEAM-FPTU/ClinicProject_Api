﻿using Clinic.MySQL.Data.Context;
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

    public async Task<IEnumerable<Domain.Commons.Entities.Doctor>> FindAllDoctorForBookingQueryAsync(CancellationToken cancellationToken)
    {
        
        return await _userDetails
            .AsNoTracking()
            .Where(doctor =>  doctor.Schedules != null && doctor.Schedules.Any(schedule => schedule.StartDate > DateTime.Now))
            .Select(selector: doctor => new Domain.Commons.Entities.Doctor()
            {
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
                            Vote = doctorSchedule.Appointment.Feedback.Vote,
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
           .ToListAsync(cancellationToken: cancellationToken);
    }
}
