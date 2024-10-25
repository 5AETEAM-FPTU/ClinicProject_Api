using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Admin.GetDoctorStaffProfile;

public sealed class GetDoctorStaffProfileRequest : IFeatureRequest<GetDoctorStaffProfileResponse>
{
    public Guid DoctorId { get; set; }
}
