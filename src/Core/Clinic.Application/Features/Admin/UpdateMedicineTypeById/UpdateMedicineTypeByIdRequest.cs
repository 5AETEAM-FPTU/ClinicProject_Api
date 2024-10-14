using System;
using Clinic.Application.Commons.Abstractions;
using FastEndpoints;

namespace Clinic.Application.Features.Admin.UpdateMedicineTypeById;

/// <summary>
///     UpdateMedicineTypeById Request
/// </summary>

public class UpdateMedicineTypeByIdRequest : IFeatureRequest<UpdateMedicineTypeByIdResponse>
{
    [BindFrom("medicineTypeId")]
    public Guid MedicineTypeId { get; set; }
    public string Name { get; init; }
    public string Constant { get; init; }
}
