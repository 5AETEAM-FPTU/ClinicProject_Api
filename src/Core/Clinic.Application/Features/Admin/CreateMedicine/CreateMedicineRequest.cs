using Clinic.Application.Commons.Abstractions;
using System;

namespace Clinic.Application.Features.Admin.CreateMedicine;

public class CreateMedicineRequest : IFeatureRequest<CreateMedicineResponse>
{
    public string MedicineName {  get; init; }
    public string Manufacture { get; init; }
    public Guid MedicineGroupId { get; init; }
    public string Ingredient { get; init; }
    public Guid MedicineTypeId { get; init; }
}

