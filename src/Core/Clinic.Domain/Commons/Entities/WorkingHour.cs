using Clinic.Domain.Commons.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Domain.Commons.Entities;


/// <summary>
///     Represent the "WorkingHours" table.
/// </summary>
public class WorkingHour : IBaseEntity, ICreatedEntity,ITemporarilyRemovedEntity,IUpdatedEntity
{
    // Primary keys.
    public Guid Id { get; set; }


    // Foreign keys.
    public Guid DoctorId { get; set; }


    // Normal columns.
    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public DateTime CreatedAt  { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public Guid UpdatedBy { get; set; }

    public DateTime RemovedAt  { get; set; }

    public Guid RemovedBy { get; set; }

    // Navigation properties.
    public Doctor Doctor { get; set; }
    public Appointment Appointment { get; set; }
}
