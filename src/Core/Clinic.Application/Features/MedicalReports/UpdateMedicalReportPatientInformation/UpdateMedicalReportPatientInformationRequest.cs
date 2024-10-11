using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.MedicalReports.UpdateMedicalReportPatientInformation;

public sealed class UpdateMedicalReportPatientInformationRequest
    : IFeatureRequest<UpdateMedicalReportPatientInformationResponse>
{
    public Guid PatientId { get; set; }
    public string FullName { get; set; }
    public DateTime Dob { get; set; }
    public string Address { get; set; }
    public string Gender { get; set; }
    public string PhoneNumber { get; set; }
}
