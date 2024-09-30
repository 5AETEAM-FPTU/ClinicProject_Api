﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.OnlinePayment.CreateNewOnlinePayment;

public class CreateNewOnlinePaymentRequest : IFeatureRequest<CreateNewOnlinePaymentResponse>
{
    public Guid PatientId { get; init; }
    public Guid AppointmentId { get; init; }
    public Double Amount { get; init; }
    public string TransactionId { get; init; }
    public string PaymentMethod { get; init; }
}
