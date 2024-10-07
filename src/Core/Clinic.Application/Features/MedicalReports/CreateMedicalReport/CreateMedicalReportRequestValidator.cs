using Clinic.Application.Commons.Abstractions;
using FluentValidation;

namespace Clinic.Application.Features.MedicalReports.CreateMedicalReport;

/// <summary>
///     CreateMedicalReportRequestValidator
/// </summary>
public sealed class CreateMedicalReportRequestValidator
    : FeatureRequestValidator<CreateMedicalReportRequest, CreateMedicalReportResponse>
{
    public CreateMedicalReportRequestValidator()
    {
        RuleFor(x => x.PatientId).NotEmpty();

        RuleFor(x => x.AppointmentId).NotEmpty();

        RuleFor(x => x.Name).NotEmpty().MinimumLength(2);

        RuleFor(x => x.MedicalHistory).MaximumLength(1000);

        RuleFor(x => x.GeneralCondition).NotEmpty();

        RuleFor(x => x.Weight).NotEmpty();
        //.Matches(@"^\d+(\.\d{1,2})?$")
        //.WithMessage("Weight must be a valid number (up to 2 decimal places).");

        RuleFor(x => x.Height).NotEmpty();
        //.Matches(@"^\d+(\.\d{1,2})?$")
        //.WithMessage("Height must be a valid number (up to 2 decimal places).");

        RuleFor(x => x.Pulse).NotEmpty();
        //.Matches(@"^\d+$")
        //.WithMessage("Pulse must be a valid number.");

        RuleFor(x => x.Temperature).NotEmpty();
        //.Matches(@"^\d+(\.\d{1,2})?$")
        //.WithMessage("Temperature must be a valid number (up to 2 decimal places).");

        RuleFor(x => x.BloodPresser).NotEmpty();
        //.Matches(@"^\d{2,3}/\d{2,3}$")
        //.WithMessage("Blood pressure must be in the format '120/80'.");

        RuleFor(x => x.Diagnosis).NotEmpty();
    }
}
