using Clinic.Application.Commons.Abstractions.UpdatePrivateDoctorInfoById;
using Clinic.Application.Features.Users.UpdatePrivateDoctorInfoById;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Users.UpdateProfileDoctor;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace Clinic.MySQL.Repositories.Doctors.UpdateProfileDoctor;

internal class UpdateDoctorDescriptionRepository : IUpdateDoctorDescriptionRepository
{
    private readonly ClinicContext _context;
    private DbSet<User> _users;

    public UpdateDoctorDescriptionRepository(ClinicContext context)
    {
        _context = context;
        _users = _context.Set<User>();
    }

    public async Task<User> GetDoctorByIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _context.Users
                .Include(user => user.Doctor)
                .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
    }

    public async Task<bool> UpdateDoctorDescriptionByIdCommandAsync
        (
        User user,
        CancellationToken cancellationToken
        )
    {
        _context.Users.Update(user);
        return await _context.SaveChangesAsync(cancellationToken) > 0;

    }

}
