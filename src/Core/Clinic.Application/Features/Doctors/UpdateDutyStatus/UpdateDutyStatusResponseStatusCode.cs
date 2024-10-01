using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.Features.Doctors.UpdateDutyStatus;

public enum UpdateDutyStatusResponseStatusCode{
    USER_IS_NOT_FOUND,
    USER_IS_TEMPORARILY_REMOVED,
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
}
