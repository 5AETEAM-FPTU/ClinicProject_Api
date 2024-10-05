using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Features.Appointments.UpdateAppointmentDepositPayment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.Features.Appointments.UpdateUserBookedAppointment;

public class UpdateUserBookedAppointmentRequest : IFeatureRequest<UpdateUserBookedAppointmentResponse>
{
    public Guid AppointmentId { get; init; }

    public Guid SelectedSlot { get; init; }

}
