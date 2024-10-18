using Clinic.Application.Commons.Abstractions;
using FastEndpoints;
using System;

namespace Clinic.Application.Features.Admin.UpdateMedicine;

/// <summary>
///     UpdateMedicine Request
/// </summary>

public class UpdateMedicineRequest : IFeatureRequest<UpdateMedicineResponse>
{
    [BindFrom("medicineId")]
    public Guid MedicineId { get; set; }
    public string MedicineName { get; init; }
    public string Manufacture { get; init; }
    public Guid MedicineGroupId { get; init; }
    public string Ingredient { get; init; }
    public Guid MedicineTypeId { get; init; }
}
