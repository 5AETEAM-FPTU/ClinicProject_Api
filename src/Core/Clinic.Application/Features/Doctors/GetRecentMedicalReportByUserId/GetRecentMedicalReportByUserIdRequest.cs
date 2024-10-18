using Clinic.Application.Commons.Abstractions;
using System;

namespace Clinic.Application.Features.Doctors.GetRecentMedicalReportByUserId;

/// <summary>
///     GetRecentMedicalReportByUserId Request
/// </summary>
public class GetRecentMedicalReportByUserIdRequest : IFeatureRequest<GetRecentMedicalReportByUserIdResponse> {
    public Guid UserId { get; init; }
    public int PageIndex { get; init; }
    public int PageSize { get; init; }
}

