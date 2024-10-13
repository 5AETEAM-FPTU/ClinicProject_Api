using Clinic.Application.Commons.Abstractions;
using FastEndpoints;
using System;

namespace Clinic.Application.Features.Admin.DeleteMedicineById;

/// <summary>
///     DeleteMedicineById Request
/// </summary>

public class DeleteMedicineByIdRequest : IFeatureRequest<DeleteMedicineByIdResponse>
{
    [BindFrom("medicineId")]
    public Guid MedicineId { get; set; }
}

