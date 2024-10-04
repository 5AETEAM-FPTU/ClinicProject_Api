using Clinic.Application.Commons.Abstractions;
using System.Collections.Generic;
using System;

namespace Clinic.Application.Features.Users.GetConsultationOverview;

/// <summary>
///     GetConsultationOverview Response
/// </summary>
public class GetConsultationOverviewResponse : IFeatureResponse
{
    public GetConsultationOverviewResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public IEnumerable<PendingConsultation> PendingConsultations { get; init; }
        public IEnumerable<DoneConsultation> DoneConsultations { get; init; }
        public sealed class PendingConsultation
        {
            public Guid PendingConsultationId { get; init; }
            public string Title { get; init; }
            public string Content { get; init; }
        }
        public sealed class DoneConsultation
        {
            public Guid DoneConsultationId { get; init; }
            public string DoctorName { get; init; }
            public string Title { get; init; }
            public string Content { get; init; }
        }
    }
}
