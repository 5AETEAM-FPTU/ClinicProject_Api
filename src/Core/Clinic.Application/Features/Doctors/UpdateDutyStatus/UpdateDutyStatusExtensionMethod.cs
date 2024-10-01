using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.Features.Doctors.UpdateDutyStatus;

public static class UpdateDutyStatusExtensionMethod
{
    public static string ToAppCode(this UpdateDutyStatusResponseStatusCode statusCode)
    {
        return $"{nameof(UpdateDutyStatus)}Feature: {statusCode}";
    }
}
