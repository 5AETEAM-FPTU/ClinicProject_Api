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
    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public ResponseAppointment Appointment { get; init; }
        public sealed class ResponseAppointment
        {
            public Guid Id { get; init; }
            public DateTime ExaminationDate { get; init; }
            public bool DepositPayment { get; init; }
        }
    }
}
