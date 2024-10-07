using Clinic.Application.Commons.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.Features.Appointments.UpdateUserBookedAppointment;

public class UpdateUserBookedAppointmentResponse : IFeatureResponse
{
   public UpdateUserBookedAppointmentResponseStatusCode StatusCode { get; init; }
}
