using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities.Base;

namespace Clinic.Domain.Commons.Entities;

/// <summary>
///     Represent the "Genders" enum.
/// </summary>
public class Gender : IBaseEntity
{
    // Primary keys.
    public Guid Id { get; set; }

    // Normal properties.
    public string Name { get; set; }

    public string Constant { get; set; }

    // Navigation collection.
    public IEnumerable<User> Users { get; set; }
}
