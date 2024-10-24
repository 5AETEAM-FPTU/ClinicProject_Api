using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.Features.Admin.GetDoctorStaffProfile;

public enum GetDoctorStaffProfileResponseStatusCode
{
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAILED,
    USER_IS_NOT_FOUND,
    ROLE_IS_NOT_PERMISSION,
}
