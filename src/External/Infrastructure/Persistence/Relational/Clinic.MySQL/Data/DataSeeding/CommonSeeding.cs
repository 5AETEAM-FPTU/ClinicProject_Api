using System;
using System.Collections.Generic;
using Clinic.Application.Commons.Constance;
using Clinic.Domain.Commons.Entities;

namespace Clinic.MySQL.Data.DataSeeding;

internal class CommonSeeding
{
    public static User InitAdmin()
    {
        User admin =
            new()
            {
                Id = EnumConstant.User.AdminId,
                UserName = "admin",
                Email = "nvdatdz8b@gmail.com",
                GenderId = EnumConstant.Gender.MALE,
                Avatar =
                    "https://firebasestorage.googleapis.com/v0/b/clinic-dab90.appspot.com/o/avatardefault_92824%20(1).jpg?alt=media&token=9f3a05d0-b63b-4fbd-9f20-0ed2fa22009f&fbclid=IwY2xjawFXVLRleHRuA2FlbQIxMAABHd48QMtiZUjUXWNb4Pd5zqpVbhjFOIGGZYvaLwcqKxRTGRQIZFN3LFNeBg_aem_rDaIQ1ITYked594N7-pCnw",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
                RemovedAt = CommonConstant.MIN_DATE_TIME,
                RemovedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
                UpdatedAt = CommonConstant.MIN_DATE_TIME,
                UpdatedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
            };

        return admin;
    }

    public static User InitStaff()
    {
        User staff =
            new()
            {
                Id = EnumConstant.User.StaffId,
                UserName = "staff",
                Email = "vuvo070403@gmail.com",
                GenderId = EnumConstant.Gender.MALE,
                Avatar =
                    "https://firebasestorage.googleapis.com/v0/b/clinic-dab90.appspot.com/o/avatardefault_92824%20(1).jpg?alt=media&token=9f3a05d0-b63b-4fbd-9f20-0ed2fa22009f&fbclid=IwY2xjawFXVLRleHRuA2FlbQIxMAABHd48QMtiZUjUXWNb4Pd5zqpVbhjFOIGGZYvaLwcqKxRTGRQIZFN3LFNeBg_aem_rDaIQ1ITYked594N7-pCnw",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
                RemovedAt = CommonConstant.MIN_DATE_TIME,
                RemovedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
                UpdatedAt = CommonConstant.MIN_DATE_TIME,
                UpdatedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
                Doctor = new()
                {
                    UserId = EnumConstant.User.StaffId,
                    Achievement =
                        "Anh da dat duoc thanh tuu to lon ve nhan cach lan con nguoi la mot tinh hoa cua nhan loa can nhan giong gap",
                    Address = "Quang Nam",
                    DOB = new DateTime(2003, 2, 2),
                    Description =
                        "Anh da dat duoc thanh tuu to lon ve nhan cach lan con nguoi la mot tinh hoa cua nhan loa can nhan giong gap",
                    PositionId = EnumConstant.Position.HEALTHCARESTAFF_ID,
                    DoctorSpecialties =
                    [
                        new()
                        {
                            SpecialtyID = EnumConstant.Specialty.GENERAL_INTERNAL_MEDICINE,
                            DoctorId = EnumConstant.User.DoctorId
                        }
                    ],
                }
            };

        return staff;
    }

    public static User InitUser()
    {
        User staff =
            new()
            {
                Id = EnumConstant.User.UserId,
                UserName = "user",
                Email = "quoch147@gmail.com",
                GenderId = EnumConstant.Gender.MALE,
                Avatar =
                    "https://firebasestorage.googleapis.com/v0/b/clinic-dab90.appspot.com/o/avatardefault_92824%20(1).jpg?alt=media&token=9f3a05d0-b63b-4fbd-9f20-0ed2fa22009f&fbclid=IwY2xjawFXVLRleHRuA2FlbQIxMAABHd48QMtiZUjUXWNb4Pd5zqpVbhjFOIGGZYvaLwcqKxRTGRQIZFN3LFNeBg_aem_rDaIQ1ITYked594N7-pCnw",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
                RemovedAt = CommonConstant.MIN_DATE_TIME,
                RemovedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
                UpdatedAt = CommonConstant.MIN_DATE_TIME,
                UpdatedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
                Patient = new()
                {
                    UserId = EnumConstant.User.UserId,
                    Address = "Tan Thuy, Le Thuy, Quang Binh",
                    DOB = new DateTime(2004, 2, 2),
                    Description =
                        "Anh da dat duoc thanh tuu to lon ve nhan cach lan con nguoi la mot tinh hoa cua nhan loa can nhan giong gap",
                }
            };

        return staff;
    }

    public static User InitDoctor()
    {
        User staff =
            new()
            {
                Id = EnumConstant.User.DoctorId,
                UserName = "doctor",
                Email = "chauthanhdat2000@gmail.com",
                GenderId = EnumConstant.Gender.MALE,
                Avatar =
                    "https://firebasestorage.googleapis.com/v0/b/clinic-dab90.appspot.com/o/avatardefault_92824%20(1).jpg?alt=media&token=9f3a05d0-b63b-4fbd-9f20-0ed2fa22009f&fbclid=IwY2xjawFXVLRleHRuA2FlbQIxMAABHd48QMtiZUjUXWNb4Pd5zqpVbhjFOIGGZYvaLwcqKxRTGRQIZFN3LFNeBg_aem_rDaIQ1ITYked594N7-pCnw",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
                RemovedAt = CommonConstant.MIN_DATE_TIME,
                RemovedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
                UpdatedAt = CommonConstant.MIN_DATE_TIME,
                UpdatedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
                Doctor = new()
                {
                    UserId = EnumConstant.User.DoctorId,
                    Achievement =
                        "Anh da dat duoc thanh tuu to lon ve nhan cach lan con nguoi la mot tinh hoa cua nhan loa can nhan giong gap",
                    Address = "Quang Nam",
                    DOB = new DateTime(2003, 2, 2),
                    Description =
                        "Anh da dat duoc thanh tuu to lon ve nhan cach lan con nguoi la mot tinh hoa cua nhan loa can nhan giong gap",

                    PositionId = EnumConstant.Position.DOCTOR_ID,
                    DoctorSpecialties =
                    [
                        new()
                        {
                            SpecialtyID = EnumConstant.Specialty.GASTROENTEROLOGY,
                            DoctorId = EnumConstant.User.DoctorId
                        },
                        new()
                        {
                            SpecialtyID = EnumConstant.Specialty.RHEUMATOLOGY,
                            DoctorId = EnumConstant.User.DoctorId
                        },
                        new()
                        {
                            SpecialtyID = EnumConstant.Specialty.UROLOGY,
                            DoctorId = EnumConstant.User.DoctorId
                        },
                    ],
                },
            };

        return staff;
    }

    public static List<Role> InitNewRoles()
    {
        Dictionary<Guid, string> newRoleNames = [];

        newRoleNames.Add(key: EnumConstant.Role.DoctorRole, value: "doctor");
        newRoleNames.Add(key: EnumConstant.Role.StaffRole, value: "staff");
        newRoleNames.Add(key: EnumConstant.Role.PatienRole, value: "user");
        newRoleNames.Add(key: EnumConstant.Role.AdminRole, value: "admin");

        List<Role> newRoles = [];

        foreach (var newRoleName in newRoleNames)
        {
            Role newRole =
                new()
                {
                    Id = newRoleName.Key,
                    Name = newRoleName.Value,
                    RoleDetail = new()
                    {
                        RoleId = newRoleName.Key,
                        CreatedAt = DateTime.UtcNow,
                        CreatedBy = EnumConstant.User.AdminId,
                        UpdatedAt = CommonConstant.MIN_DATE_TIME,
                        UpdatedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
                        RemovedAt = CommonConstant.MIN_DATE_TIME,
                        RemovedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
                    }
                };

            newRoles.Add(item: newRole);
        }

        return newRoles;
    }
}
