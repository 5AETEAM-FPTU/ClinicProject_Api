using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.Features.Admin.GetDoctorStaffProfile;

public static class GetDoctorStaffProfileExtensionMethod
{
    public static string ToAppCode(this GetDoctorStaffProfileResponseStatusCode statusCode)
    {
        return $"{nameof(GetDoctorStaffProfile)}Feature: {statusCode}";
    }
}
