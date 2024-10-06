using Clinic.Application.Features.Appointments.UpdateUserBookedAppointment;
using System.Collections.Generic;
using System;
using Clinic.Application.Features.Appointments.UpdateUserBookedAppointment;

namespace Clinic.WebAPI.EndPoints.Appointments.UpdateUserBookedAppointment.HttpResponseMapper;

internal sealed class UpdateUserBookedAppointmentHttpResponseMapper
{
    private static UpdateUserBookedAppointmentHttpResponseManager _updateUserBookedAppointmentHttpResponseManager;

    internal static UpdateUserBookedAppointmentHttpResponseManager Get()
    {
        return _updateUserBookedAppointmentHttpResponseManager ??= new();
    }
}
