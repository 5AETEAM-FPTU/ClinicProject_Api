using Clinic.Application.Commons.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.Features.Doctors.UpdateDutyStatus;

public sealed class UpdateDutyStatusResponse : IFeatureResponse
{
    public UpdateDutyStatusResponseStatusCode StatusCode { get; init; }
}
