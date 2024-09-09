using System;
using Clinic.Domain.Commons.Entities.Base;
using Microsoft.AspNetCore.Identity;

namespace Clinic.Domain.Commons.Entities;

/// <summary>
///     Represent the "Users" table.
/// </summary>
public class User : IdentityUser<Guid>, IBaseEntity { }
