using System;
using System.Collections.Generic;
using Clinic.Domain.Commons.Entities.Base;

namespace Clinic.Domain.Commons.Entities;

/// <summary>
///     Represent the "ChatRoom" table.
/// </summary>
public class ChatRoom : IBaseEntity, ICreatedEntity, IUpdatedEntity
{
    // Primary keys.
    public Guid Id { get; set; }

    // Normal columns.
    public string LastMessage { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public Guid UpdatedBy { get; set; }

    // Foreign keys.
    public Guid PatientId { get; set; }

    public Guid DoctorId { get; set; }

    // Navigation properties.
    public Patient Patient { get; set; }

    public Doctor Doctor { get; set; }

    // Navigation Collections.
    public IEnumerable<ChatContent> ChatContents { get; set; }
}
