using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.MedicalReports.UpdateMainMedicalReportInformation;

public sealed class UpdateMainMedicalReportInformationResponse : IFeatureResponse
{
    public UpdateMainMedicalReportInformationResponseStatusCode StatusCode { get; set; }
}
