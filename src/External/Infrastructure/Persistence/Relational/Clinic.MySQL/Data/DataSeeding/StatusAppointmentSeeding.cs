using System;
using System.Collections.Generic;
using Clinic.Domain.Commons.Entities;

namespace Clinic.MySQL.Data.DataSeeding;

internal class AppointmentStatusSeeding
{
    public static List<AppointmentStatus> InitAppointmentStatuses()
    {
        return
        [
            new()
            {
                Id = EnumConstant.AppointmentStatus.PENDING,
                StatusName = "Đang chờ",
                Constant = "Pending",
            },
            new()
            {
                Id = EnumConstant.AppointmentStatus.COMPLETED,
                StatusName = "Đã Khám xong",
                Constant = "Completed",
            },
            new()
            {
                Id = EnumConstant.AppointmentStatus.NO_SHOW,
                StatusName = "Vắng mặt",
                Constant = "No-Show",
            }
        ];
    }
}
