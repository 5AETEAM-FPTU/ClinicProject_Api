using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Commons.Pagination;
using System;


namespace Clinic.Application.Features.Feedbacks.DoctorGetAllFeedbacks;

public class DoctorGetAllFeedBackResponse : IFeatureResponse
{
    public DoctorGetAllFeedBackResponseStatusCode StatusCode { get; set; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public PaginationResponse<Feedback> Feedbacks { get; set; }
        public sealed class Feedback
        {
            public Guid Id { get; set; }
            public string Comment { get; set; }
            public int Vote { get; set; }
            public string PatientName { get; set; }
            public string AvatarUrl { get; set; }
            public DateTime CreatedAt { get; set; }
        }

        public double Rating { get; set; }
        public int TotalOfRating { get; set; }

    }

}
