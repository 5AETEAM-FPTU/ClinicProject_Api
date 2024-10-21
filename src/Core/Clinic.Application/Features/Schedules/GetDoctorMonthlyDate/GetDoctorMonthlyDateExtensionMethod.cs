using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.Features.Schedules.GetDoctorMonthlyDate;

public static class GetDoctorMonthlyDateExtensionMethod
{
    public static string ToAppCode(this GetDoctorMonthlyDateResponseStatusCode statusCode)
    {
        return $"{nameof(GetDoctorMonthlyDate)}Feature: {statusCode}";
    }
}
