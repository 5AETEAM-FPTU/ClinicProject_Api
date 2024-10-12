using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.Features.MedicalReports.UpdateMainMedicalReportInformation;

public static class UpdateMainMedicalReportInformationExtensionMethod
{
    public static string ToAppCode(
        this UpdateMainMedicalReportInformationResponseStatusCode StatusCode
    )
    {
        return $"{nameof(UpdateMainMedicalReportInformation)}Feature: {StatusCode}";
    }
}
