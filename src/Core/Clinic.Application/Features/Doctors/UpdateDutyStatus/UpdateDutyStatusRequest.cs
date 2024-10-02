using Clinic.Application.Commons.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.Features.Doctors.UpdateDutyStatus;

public sealed class UpdateDutyStatusRequest : IFeatureRequest<UpdateDutyStatusResponse>
{
    public bool Status { get; init; }

}
