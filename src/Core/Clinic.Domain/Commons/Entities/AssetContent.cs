using Clinic.Domain.Commons.Entities.Base;
using System;

namespace Clinic.Domain.Commons.Entities;

/// <summary>
///     Represent the "AssetContent" table.
/// </summary>
public class AssetContent : IBaseEntity, ICreatedEntity, ITemporarilyRemovedEntity, IUpdatedEntity
{
    // Primary keys.
    public Guid Id { get; set; }

    // Foreign keys.
    public Guid ChatContentId { get; set; }

    // Normal columns.
    public string Asset { get; set; }

    public string Type { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public Guid UpdatedBy { get; set; }

    public DateTime RemovedAt { get; set; }

    public Guid RemovedBy { get; set; }


    // Navigation properties.
    public ChatContent ChatContent { get; set; }
}

