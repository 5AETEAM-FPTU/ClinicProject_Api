using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.Features.OnlinePayments.CreateNewOnlinePayment;

public static class CreateNewOnlinePaymentExtensionMethod
{
    public static string ToAppCode(this CreateNewOnlinePaymentResponseStatusCode statusCode)
    {
        return $"{nameof(CreateNewOnlinePayment)}Feature: {statusCode}";
    }
}
