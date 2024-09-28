using System;
using System.Collections.Generic;
using Clinic.Domain.Commons.Entities.Base;

namespace Clinic.Domain.Commons.Entities;

/// <summary>
///     Represent the "RetreatmentType" enum.
/// </summary>
public class RetreatmentType : IBaseEntity
{
    // Primary keys.
    public Guid Id { get; set; }

    // Normal properties.
    public string Name { get; set; }

    public string Constant { get; set; }

    public IEnumerable<RetreatmentNotification> RetreatmentNotifications { get; set; }
}
