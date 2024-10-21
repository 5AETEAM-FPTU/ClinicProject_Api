using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Schedules.GetDoctorMonthlyDate;

public class GetDoctorMonthlyDateResponse : IFeatureResponse
{
    public GetDoctorMonthlyDateResponseStatusCode StatusCode { get; set; }
    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public List<DateTime> WorkingDays { get; set; }
    }
}
