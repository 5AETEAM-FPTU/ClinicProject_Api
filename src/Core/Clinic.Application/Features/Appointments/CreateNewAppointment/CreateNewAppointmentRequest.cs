using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Appointments.CreateNewAppointment;

public class CreateNewAppointmentRequest : IFeatureRequest<CreateNewAppointmentResponse>
{
    public Guid ScheduleId { get; init; }
    public bool ReExamination { get; set; }
    public DateTime ExaminationDate { get; init; }
    public bool DepositPayment { get; set; }
    public string Description { get; init; }
}
