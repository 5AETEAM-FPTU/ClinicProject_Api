using Clinic.Application.Commons.Abstractions;
using FastEndpoints;
using System;

namespace Clinic.Application.Features.Admin.DeleteMedicineGroupById;

/// <summary>
///     DeleteMedicineGroupById Request
/// </summary>

public class DeleteMedicineGroupByIdRequest : IFeatureRequest<DeleteMedicineGroupByIdResponse>
{
    [BindFrom("medicineGroupId")]
    public Guid MedicineGroupId { get; set; }
}
