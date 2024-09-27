using System;
using System.Collections.Generic;
using Clinic.Domain.Commons.Entities.Base;

namespace Clinic.Domain.Commons.Entities;

/// <summary>
///     Represent the "Doctors" table.
/// </summary>
public class Doctor : IBaseEntity
{
    // Primary keys.
    // Foreign keys.
    public Guid UserId { get; set; }

    // Normal columns.
    public DateTime DOB { get; set; }

    public string Address { get; set; }

    public string Description { get; set; }

    public string Achievement { get; set; }

    public bool IsOnDuty { get; set; }

    // Foreign keys.
    public Guid PositionId { get; set; }

    public User User { get; set; }

    public IEnumerable<DoctorSpecialty> DoctorSpecialties { get; set; }

    public Position Position { get; set; }

    // Navigation Collections.
    public IEnumerable<Schedule> Schedules { get; set; }

    public IEnumerable<ChatRoom> ChatRooms { get; set; }
}
