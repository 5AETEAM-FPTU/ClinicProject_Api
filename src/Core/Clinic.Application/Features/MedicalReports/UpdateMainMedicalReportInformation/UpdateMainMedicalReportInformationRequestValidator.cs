using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using FluentValidation;

namespace Clinic.Application.Features.MedicalReports.UpdateMainMedicalReportInformation;

public sealed class UpdateMainMedicalReportInformationRequestValidator
    : FeatureRequestValidator<
        UpdateMainMedicalReportInformationRequest,
        UpdateMainMedicalReportInformationResponse
    >
{
    public UpdateMainMedicalReportInformationRequestValidator()
    {
        RuleFor(x => x.ReportId)
            .NotEmpty()
            .Must(predicate: id => Guid.TryParse(id.ToString(), out _))
            .WithMessage("ReportId must not be empty and must be a Guid");
    }
}
