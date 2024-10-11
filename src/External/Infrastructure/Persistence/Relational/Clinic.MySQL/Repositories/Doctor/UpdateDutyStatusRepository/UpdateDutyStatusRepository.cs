using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Doctors.UpdateDutyStatus;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.MySQL.Repositories.Doctor.UpdateDutyStatusRepository;

internal class UpdateDutyStatusRepository : IUpdateDutyStatusRepository
{
    private readonly ClinicContext _context;

    public UpdateDutyStatusRepository(ClinicContext context)
    {
        _context = context;
    }

    public async Task<User> GetDoctorById(Guid userId)
    {
        return await _context.Users.Include(u => u.Doctor).FirstOrDefaultAsync(u => u.Id == userId);
    }
    public async Task<bool> UpdateDutyStatusCommandAsync(User doctor, bool status, CancellationToken cancellationToken)
    {
        try
        {
            doctor.Doctor.IsOnDuty = status;
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception ex) { 
            return false;
        }
        
    }
}
