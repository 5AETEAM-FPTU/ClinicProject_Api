using System;

namespace Clinic.Application.Commons.Payment;

/// <summary>
///     Represent the payment data input model.
/// </summary>
public class PaymentModel
{
    public string IPAddress { get; set; }

    public int Amount { get; set; }

    public string OrderInfo { get; set; }

    public DateTime CreatedDate { get; set; }

    public string TxnRef { get; set; }

    public string AppointmentId { get; set; }
}
