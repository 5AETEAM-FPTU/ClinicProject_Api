using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.Features.Appointments.CreateNewAppointment;

public static class CreateNewAppointmentExtensionMethod
{
    public static string ToAppCode(this CreateNewAppointmentResponseStatusCode statusCode)
    {
        return $"{nameof(CreateNewAppointment)}Feature: {statusCode}";
    }
}
