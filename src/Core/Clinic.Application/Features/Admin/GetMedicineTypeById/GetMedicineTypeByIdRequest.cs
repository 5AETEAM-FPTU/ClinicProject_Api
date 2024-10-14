using Clinic.Application.Commons.Abstractions;
using System;

namespace Clinic.Application.Features.Admin.GetMedicineTypeById;

public class GetMedicineTypeByIdRequest : IFeatureRequest<GetMedicineTypeByIdResponse>
{
    public Guid MedicineTypeId { get; init; }
}
