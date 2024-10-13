using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Admin.DeleteMedicineById;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;
using static Clinic.Application.Features.Appointments.GetAbsentAppointment.GetAbsentAppointmentResponse.Body.AppointmentDetail;

namespace Clinic.MySQL.Repositories.Admin.DeleteMedicineById;

public class DeleteMedicineByIdRepository : IDeleteMedicineByIdRepository
{
    private readonly ClinicContext _context;
    private DbSet<Medicine> _medicines;

    public DeleteMedicineByIdRepository(ClinicContext context)
    {
        _context = context;
        _medicines = context.Set<Medicine>();
    }

    public async Task<bool> DeleteMedicineByIdCommandAsync(
        Guid medicineId,
        CancellationToken cancellationToken
    )
    {
        var isDeletedSuccessfully = false;

        try
        {
            await _medicines.Where(medicine => medicine.Id == medicineId).ExecuteDeleteAsync();
            await _context.SaveChangesAsync(cancellationToken);
            isDeletedSuccessfully = true;
        }
        catch
        {
            isDeletedSuccessfully = false;
        }

        return isDeletedSuccessfully;
    }

    public async Task<bool> IsMedicineExist(Guid medicineId, CancellationToken cancellationToken)
    {
        return await _medicines.AnyAsync(medicine => medicine.Id == medicineId, cancellationToken);
    }
}
