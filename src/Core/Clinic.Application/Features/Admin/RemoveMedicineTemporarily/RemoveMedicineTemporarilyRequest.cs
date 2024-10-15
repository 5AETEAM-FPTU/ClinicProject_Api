using Clinic.Application.Commons.Abstractions;
using FastEndpoints;
using System;

namespace Clinic.Application.Features.Admin.RemoveMedicineTemporarily;

/// <summary>
///     RemoveMedicineTemporarilyl Request
/// </summary>

public class RemoveMedicineTemporarilyRequest : IFeatureRequest<RemoveMedicineTemporarilyResponse>
{
    [BindFrom("medicineId")]
    public Guid MedicineId { get; set; }
}
