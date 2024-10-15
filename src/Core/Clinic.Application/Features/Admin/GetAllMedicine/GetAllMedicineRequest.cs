using System;
using Clinic.Application.Commons.Abstractions;
using FastEndpoints;

namespace Clinic.Application.Features.Admin.GetAllMedicine;

/// <summary>
///     GetAllMedicine Request
/// </summary>
public class GetAllMedicineRequest : IFeatureRequest<GetAllMedicineResponse>
{
    public int PageIndex { get; init; }

    public int PageSize { get; init; }

    [BindFrom("medicineName")]
    public string? Name { get; init; } = "";

    [BindFrom("medicineTypeId")]
    public Guid? MedicineTypeId { get; init; }

    [BindFrom("medicineGroupId")]
    public Guid? MedicineGroupId { get; init; }
}
