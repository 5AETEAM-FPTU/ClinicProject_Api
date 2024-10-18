using System;
using System.Collections.Generic;
using Clinic.Domain.Commons.Entities.Base;

namespace Clinic.Domain.Commons.Entities;

/// <summary>
///     Represent the "Patients" table.
/// </summary>
public class Patient : IBaseEntity
{
    // Primary keys.
    // Foreign keys.
    public Guid UserId { get; set; }

    // Normal properties.
    public DateTime DOB { get; set; }

    public string Address { get; set; }

    public string Description { get; set; }

    // Navigation properties.
    public User User { get; set; }

    // Navigation collections.
    public IEnumerable<Appointment> Appointments { get; set; }

    public IEnumerable<QueueRoom> QueueRooms { get; set; }

    public IEnumerable<ChatRoom> ChatRooms { get; set; }

    public IEnumerable<RetreatmentNotification> RetreatmentNotifications { get; set; }
}
