using System;
using Clinic.Domain.Commons.Entities.Base;

namespace Clinic.Domain.Commons.Entities;

/// <summary>
///     Represent the "Assets" table.
/// </summary>
public class Asset : IBaseEntity, ITemporarilyRemovedEntity
{
    // Primary keys.
    public Guid Id { get; set; }

    // Normal columns.
    public string FileName { get; set; }

    public string FilePath { get; set; }

    public string Type { get; set; }

    public DateTime RemovedAt { get; set; }

    public Guid RemovedBy { get; set; }

    // Foreign key.
    public Guid ChatContentId { get; set; }

    // Navigation properties.
    public ChatContent ChatContent { get; set; }
}
