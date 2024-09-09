using System;

namespace Clinic.Domain.Commons.Entities.Base;

/// <summary>
///     Use in case the entity requires the information
///     about the person removing the entity temporarily
///     and the time is it temporarily removed.
/// </summary>
internal abstract class ITemporarilyRemovedEntity
{
    /// <summary>
    ///     When is entity temporarily removed.
    /// </summary>
    DateTime RemovedAt { get; set; }

    /// <summary>
    ///     Id of the entity remover.
    /// </summary>
    Guid RemovedBy { get; set; }
}
