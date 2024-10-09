using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Commons.Pagination;
using Clinic.Application.Features.VNPays.CreatePaymentLink;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
namespace Clinic.Application.Features.Users.VNPays.CreatePaymentLink;

/// <summary>
///     GetAllDoctors Response
/// </summary>
public class CreatePaymentLinkResponse : IFeatureResponse
{
    public CreatePaymentLinkResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public string PaymentUrl {get; set; }
    }
}

