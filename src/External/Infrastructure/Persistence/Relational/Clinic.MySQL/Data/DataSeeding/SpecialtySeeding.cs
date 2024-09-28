using System;
using System.Collections.Generic;
using Clinic.Domain.Commons.Entities;

namespace Clinic.MySQL.Data.DataSeeding;

internal class SpecialtySeeding
{
    public static List<Specialty> InitSpecialties()
    {
        return
        [
            new()
            {
                Id = EnumConstant.Specialty.CARDIOLOGY,
                Name = "Nội tim mạch",
                Constant = "Cardiology",
            },
            new()
            {
                Id = EnumConstant.Specialty.INFECTIOUS_DISEASES,
                Name = "Nội truyền nhiễm",
                Constant = "Infectious Diseases",
            },
            new()
            {
                Id = EnumConstant.Specialty.GENERAL_INTERNAL_MEDICINE,
                Name = "Nội tổng quát",
                Constant = "General Internal Medicine",
            },
            new()
            {
                Id = EnumConstant.Specialty.GASTROENTEROLOGY,
                Name = "Nội tiêu hóa",
                Constant = "Gastroenterology",
            },
            new()
            {
                Id = EnumConstant.Specialty.UROLOGY,
                Name = "Nội tiết niệu",
                Constant = "Urology",
            },
            new()
            {
                Id = EnumConstant.Specialty.RHEUMATOLOGY,
                Name = "Nội cơ xương khớp",
                Constant = "Rheumatology ",
            },
        ];
    }
}
