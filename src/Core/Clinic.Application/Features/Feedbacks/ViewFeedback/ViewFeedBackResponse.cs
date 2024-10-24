using Clinic.Application.Commons.Abstractions;
using System;
using System.Collections.Generic;

namespace Clinic.Application.Features.Feedbacks.ViewFeedback;

public sealed class ViewFeedBackResponse : IFeatureResponse
{
    public ViewFeedBackResponseStatusCode StatusCode { get; set; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public FeedbackDetail Feedback { get; set; }

        public sealed class FeedbackDetail
        {
            public Guid Id { get; set; }
            public String Comment { get; set; }
            public int Vote { get; set; }

        }

        public DoctorInfo Doctor { get; set; }
        public sealed class DoctorInfo
        {
            public double Rating { get; set; }
            public string Fullname {  get; set; }
            public string AvatarUrl { get; set; }
            public IEnumerable<Specialty> Specialties { get; set; }

            public sealed class Specialty
            {
                public Guid Id { get; set; }
                public string Name { get; set; }
                public string Constant { get; set; }
            }
        }

    }

}
