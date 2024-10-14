using System;
using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Admin.GetMedicineGroupById;

public class GetMedicineGroupByIdRequest : IFeatureRequest<GetMedicineGroupByIdResponse>
{
    public Guid MedicineGroupId { get; init; }
}
