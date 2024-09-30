using System;
using Clinic.Domain.Commons.Entities.Base;

namespace Clinic.Domain.Commons.Entities;

/// <summary>
///     Represent the "OnlinePayment" table.
/// </summary>
public class OnlinePayment : IBaseEntity, ICreatedEntity, IUpdatedEntity
{
    // Primary keys.
    public Guid Id { get; set; }

    // Normal properties.
    public string TransactionID { get; set; }

    public int Amount { get; set; }

    public string PaymentMethod { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public Guid UpdatedBy { get; set; }

    // Foreign keys.
    public Guid AppointmentId { get; set; }

    // Navigation properties.
    public Appointment Appointment { get; set; }
}
