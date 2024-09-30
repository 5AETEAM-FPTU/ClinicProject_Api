using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Appointments.CreateNewAppointment;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.Appointments;

internal class CreateNewAppointmentRepository : ICreateNewAppointmentRepository
{
    private readonly ClinicContext _context;

    public CreateNewAppointmentRepository(ClinicContext context)
    {
        _context = context;
    }
}
