using System;
using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Feedbacks.DoctorGetAllFeedbacks;

public class DoctorGetAllFeedBackRequest : IFeatureRequest<DoctorGetAllFeedBackResponse>
{
    public int PageIndex { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public int? Vote { get; set; }                  // search field
}
