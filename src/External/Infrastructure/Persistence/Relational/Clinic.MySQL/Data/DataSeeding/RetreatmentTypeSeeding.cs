using System;
using System.Collections.Generic;
using Clinic.Domain.Commons.Entities;

namespace Clinic.MySQL.Data.DataSeeding;

internal class RetreatmentTypeSeeding
{
    public static List<RetreatmentType> InitRetreatmentTypes()
    {
        return
        [
            new()
            {
                Id = EnumConstant.RetreatmentType.ADDITIONAL_TESTS,
                Name = "Xét nghiệm bổ sung",
                Constant = "Additional tests",
            },
            new()
            {
                Id = EnumConstant.RetreatmentType.LONG_TERM,
                Name = "Điều trị lâu dài",
                Constant = "Long-term treatment",
            }
        ];
    }
}
