using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Schedules.GetDoctorScheduleByDate;

public sealed class GetDoctorScheduleByDateResponse : IFeatureResponse
{
    public GetDoctorScheduleByDateResponseStatusCode StatusCode;
    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public List<TimeSlot> TimeSlots { get; init; }

        public sealed class TimeSlot
        {
            public Guid SlotId { get; set; }

            public DateTime StartTime { get; set; }

            public DateTime EndTime { get; set; }

            public bool IsHadAppointment { get; set; }
        }
    }
}
