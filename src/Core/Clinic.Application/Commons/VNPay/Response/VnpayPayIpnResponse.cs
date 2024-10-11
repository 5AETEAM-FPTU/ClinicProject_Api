using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Features.VNPays.VNPayIPN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.Commons.VNPay.Response;

public class VnpayPayIpnResponse : IFeatureResponse
{
    public VNPayIPNResponseStatusCode StatusCode { get; set; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public string RspCode { get; set; }
        public string Message { get; set; }
    }

}
