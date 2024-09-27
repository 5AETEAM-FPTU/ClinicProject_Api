using System;
using System.Collections.Generic;
using Clinic.Domain.Commons.Entities;

namespace Clinic.MySQL.Data.DataSeeding;

internal class GenderSeeding
{
    public static List<Gender> InitGenders()
    {
        return
        [
            new()
            {
                Id = EnumConstant.Gender.MALE,
                Name = "Nam",
                Constant = "Male",
            },
            new()
            {
                Id = EnumConstant.Gender.FEMALE,
                Name = "Nữ",
                Constant = "Female",
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Khác",
                Constant = "Other",
            }
        ];
    }
}
