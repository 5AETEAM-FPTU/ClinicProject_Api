using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using FluentValidation;

namespace Clinic.Application.Features.MedicalReports.UpdateMedicalReportPatientInformation;

public sealed class UpdateMedicalReportPatientInformationRequestValidator
    : FeatureRequestValidator<
        UpdateMedicalReportPatientInformationRequest,
        UpdateMedicalReportPatientInformationResponse
    >
{
    public UpdateMedicalReportPatientInformationRequestValidator()
    {
        RuleFor(x => x.PatientId)
            .NotEmpty()
            .Must(predicate: id => Guid.TryParse(id.ToString(), out _))
            .WithMessage("PatientId is required and must be a valid Guid");
    }
}
