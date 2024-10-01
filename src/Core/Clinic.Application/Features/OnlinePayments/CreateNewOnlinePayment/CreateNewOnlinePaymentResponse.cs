using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.OnlinePayments.CreateNewOnlinePayment;

public sealed class CreateNewOnlinePaymentResponse : IFeatureResponse
{
    public CreateNewOnlinePaymentResponseStatusCode StatusCode { get; set; }
}
