using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.Features.Appointments.UpdateUserBookedAppointment;

public static class UpdateUserBookedAppointmentExtensionMethod
{
    public static string ToAppCode(this UpdateUserBookedAppointmentResponseStatusCode statusCode)
    {
        return $"{nameof(UpdateUserBookedAppointment)}Feature: {statusCode}";
    }
}
