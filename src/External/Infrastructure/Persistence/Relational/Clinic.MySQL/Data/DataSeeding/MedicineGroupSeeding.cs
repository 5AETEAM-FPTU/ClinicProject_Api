using System;
using System.Collections.Generic;
using Clinic.Domain.Commons.Entities;

namespace Clinic.MySQL.Data.DataSeeding;

internal class MedicineGroupSeeding
{
    public static List<MedicineGroup> InitMedicineGroups()
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
