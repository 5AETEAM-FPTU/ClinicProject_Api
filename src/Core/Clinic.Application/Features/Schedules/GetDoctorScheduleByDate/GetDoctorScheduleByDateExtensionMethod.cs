using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.Features.Schedules.GetDoctorScheduleByDate;

public static class GetDoctorScheduleByDateExtensionMethod
{
    public static string ToAppCode(this GetDoctorScheduleByDateResponseStatusCode statusCode)
    {
        return $"{nameof(GetDoctorScheduleByDate)}Feature: {statusCode}";
    }
}
