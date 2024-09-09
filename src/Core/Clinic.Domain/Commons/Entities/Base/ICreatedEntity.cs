using System;

namespace Clinic.Domain.Commons.Entities.Base;

/// <summary>
///     Use in case the entity requires the information
///     about the person creating the entity and the time
///     is it created.
/// </summary>
internal abstract class ICreatedEntity
{
    /// <summary>
    ///     When is entity created.
    /// </summary>
    DateTime CreatedAt { get; set; }

    /// <summary>
    ///     Id of the entity creator.
    /// </summary>
    Guid CreatedBy { get; set; }
}
