using System;
using System.Collections.Generic;
using Clinic.Domain.Commons.Entities;

namespace Clinic.MySQL.Data.DataSeeding;

internal class PositionSeeding
{
    public static List<Position> InitPositions()
    {
        return
        [
            new()
            {
                Id = EnumConstant.Position.DOCTOR_ID,
                Name = "Bác sĩ",
                Constant = "Doctor",
            },
            new()
            {
                Id = EnumConstant.Position.HEALTHCARESTAFF_ID,
                Name = "Nhân viên y tế",
                Constant = "Healthcare staff",
            },
            new()
            {
                Id = EnumConstant.Position.MASTERDOCTOR_ID,
                Name = "Thạc sĩ",
                Constant = "Master doctor",
            }
        ];
    }
}
