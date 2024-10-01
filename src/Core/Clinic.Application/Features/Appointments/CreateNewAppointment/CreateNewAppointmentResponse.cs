using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Appointments.CreateNewAppointment;

public sealed class CreateNewAppointmentResponse : IFeatureResponse
{
    public CreateNewAppointmentResponseStatusCode StatusCode { get; set; }
}
