using System;
using Clinic.Application.Commons.Abstractions;
using FastEndpoints;

namespace Clinic.Application.Features.MedicalReports.GetMedicalReportById;

public class GetMedicalRepostByIdRequest : IFeatureRequest<IFeatureResponse> {
    [BindFrom("doctorId")]
    public Guid? DoctorId { get; set; }
}