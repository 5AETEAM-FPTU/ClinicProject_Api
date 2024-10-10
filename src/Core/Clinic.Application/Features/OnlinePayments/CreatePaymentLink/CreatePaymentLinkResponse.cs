using System;
using System.Collections.Generic;
using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Commons.Pagination;
using Microsoft.AspNetCore.Routing;

namespace Clinic.Application.Features.OnlinePayments.CreatePaymentLink;

/// <summary>
///     GetAllDoctors Response
/// </summary>
public class CreatePaymentLinkResponse : IFeatureResponse
{
    public CreatePaymentLinkResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public string PaymentUrl { get; set; }
    }
}
