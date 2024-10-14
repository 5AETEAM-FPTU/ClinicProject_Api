using System;
using Clinic.Application.Commons.Abstractions;
using FastEndpoints;

namespace Clinic.Application.Features.Admin.UpdateMedicineGroupById;

/// <summary>
///     UpdateMedicineGroupById Request
/// </summary>

public class UpdateMedicineGroupByIdRequest : IFeatureRequest<UpdateMedicineGroupByIdResponse>
{
    [BindFrom("medicineGroupId")]
    public Guid MedicineGroupId { get; set; }
    public string Name { get; init; }
    public string Constant { get; init; }
}
