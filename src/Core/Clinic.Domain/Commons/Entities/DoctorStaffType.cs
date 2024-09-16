using Clinic.Domain.Commons.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Domain.Commons.Entities;


/// <summary>
///     Represent the "DoctorStaffTypes" table.
/// </summary>
public class DoctorStaffType : IBaseEntity, ICreatedEntity, ITemporarilyRemovedEntity, IUpdatedEntity
{
    // Primary keys.
    public Guid Id { get; set; }

    // Foreign keys
    public Guid UserId { get; set; }    

    // Normal columns.
    public string TypeName { get; set; }

    public string Constant { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public Guid UpdatedBy { get; set; }

    public DateTime RemovedAt { get; set; }

    public Guid RemovedBy { get; set; }

    // Navigation properties.
    public Doctor Doctor { get; set; }
    public User User { get; set; }
}
