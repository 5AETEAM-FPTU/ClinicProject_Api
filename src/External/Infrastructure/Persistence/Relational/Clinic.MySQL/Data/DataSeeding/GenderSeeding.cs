using System;
using System.Collections.Generic;
using Clinic.Application.Commons.Constance;
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
                Id = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
                Name = "Khác",
                Constant = "Other",
            },
        ];
    }
}
