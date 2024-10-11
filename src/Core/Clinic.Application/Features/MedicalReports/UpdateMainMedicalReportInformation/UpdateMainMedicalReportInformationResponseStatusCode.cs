using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.Features.MedicalReports.UpdateMainMedicalReportInformation;

public enum UpdateMainMedicalReportInformationResponseStatusCode
{
    UNAUTHORIZED,
    FORBIDDEN,
    NOT_FOUND,
    OPERATION_SUCCESSFUL,
    DATABASE_OPERATION_FAILED,
}
