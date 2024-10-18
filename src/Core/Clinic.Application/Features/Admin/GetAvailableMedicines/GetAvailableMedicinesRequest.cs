using System;
using Clinic.Application.Commons.Abstractions;
using FastEndpoints;

namespace Clinic.Application.Features.Admin.GetAvailableMedicines;

/// <summary>
///     GetAvailableMedicines Request
/// </summary>
public class GetAvailableMedicinesRequest : IFeatureRequest<GetAvailableMedicinesResponse>
{
    [BindFrom("medicineName")]
    public string? Name { get; init; } = "";

    [BindFrom("medicineTypeId")]
    public Guid? MedicineTypeId { get; init; }

    [BindFrom("medicineGroupId")]
    public Guid? MedicineGroupId { get; init; }
}
