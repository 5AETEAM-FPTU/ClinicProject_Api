using Clinic.Application.Commons.Abstractions;
using System;

namespace Clinic.Application.Features.Admin.GetMedicineById;

public class GetMedicineByIdRequest : IFeatureRequest<GetMedicineByIdResponse>
{
    public Guid MedicineId { get; init; }
}
