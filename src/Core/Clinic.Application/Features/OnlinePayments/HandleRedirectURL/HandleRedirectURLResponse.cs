using System;
using System.Collections;
using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.OnlinePayments.HandleRedirectURL;

/// <summary>
///     HandleRedirectURL Response.
/// </summary>
public class HandleRedirectURLResponse : IFeatureResponse
{
    public HandleRedirectURLResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public string TransactionId { get; set; }
        public string Amount { get; set; }
        public string PaymentDate { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string DoctorName { get; set; }
    }
}
