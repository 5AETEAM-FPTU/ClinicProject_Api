using System;
using System.Collections.Generic;
using Clinic.Domain.Commons.Entities;

namespace Clinic.MySQL.Data.DataSeeding;

internal class MedicineTypeSeeding
{
    public static List<MedicineType> InitMedicineTypes()
    {
        return
        [
            new()
            {
                Id = Guid.NewGuid(),
                Name = "",
                Constant = "",
            },
        ];
    }
}
