﻿using Clinic.Domain.Commons.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Domain.Commons.Entities;

/// <summary>
///     Represent the "OnlinePayment" table.
/// </summary>
public class OnlinePayment : IBaseEntity, ICreatedEntity, ITemporarilyRemovedEntity, IUpdatedEntity
{
    // Primary keys.
    public Guid Id { get; set; }

    // Foreign keys.
    public Guid PatientId { get; set; }

    // Normal columns.
    public string TransactionID { get; set; }

    public int Amount { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public Guid UpdatedBy { get; set; }

    public DateTime RemovedAt { get; set; }

    public Guid RemovedBy { get; set; }


    // Navigation properties.
    public Patient Patient { get; set; }
    public Appointment Appointment { get; set; }
}

