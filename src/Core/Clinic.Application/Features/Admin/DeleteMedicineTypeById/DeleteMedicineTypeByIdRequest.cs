using Clinic.Application.Commons.Abstractions;
using FastEndpoints;
using System;


namespace Clinic.Application.Features.Admin.DeleteMedicineTypeById;

/// <summary>
///     DeleteMedicineTypeById Request
/// </summary>

public class DeleteMedicineTypeByIdRequest : IFeatureRequest<DeleteMedicineTypeByIdResponse>
{
    [BindFrom("medicineTypeId")]
    public Guid MedicineTypeId { get; set; }
}
