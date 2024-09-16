﻿using Clinic.Domain.Commons.Entities.Base;
using System;
using System.Collections.Generic;

namespace Clinic.Domain.Commons.Entities;


/// <summary>
///     Represent the "Doctors" table.
/// </summary>
public class Doctor : IBaseEntity, ICreatedEntity, ITemporarilyRemovedEntity, IUpdatedEntity
{
    // Primary keys.
    // Foreign keys.
    public Guid Id { get; set; }

    // Foreign keys.
    public Guid DoctorStaffTypeId { get; set; }

    // Normal columns.
    public string FullName { get; set; }

    public string Gender { get; set; }

    public DateTime DOB { get; set; }

    public string Position { get; set; }

    public string Specialty { get; set; }

    public string Adress { get; set; }

    public string Description { get; set; }

    public string Avatar { get; set; }

    public string Achievement { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public Guid UpdatedBy { get; set; }

    public DateTime RemovedAt { get; set; }

    public Guid RemovedBy { get; set; }


    // Navigation properties.
    public DoctorStaffType DoctorStaffType { get; set; }
    public User User { get; set; }
    public IEnumerable<WorkingHour> WorkingHours { get; set; }
    public IEnumerable<ChatRoom> ChatRooms { get; set; }
}
