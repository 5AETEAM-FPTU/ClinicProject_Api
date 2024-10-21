using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.Features.Schedules.GetDoctorMonthlyDate;

public enum GetDoctorMonthlyDateResponseStatusCode
{
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAILED,
    DOCTOR_IS_NOT_FOUND,
}
