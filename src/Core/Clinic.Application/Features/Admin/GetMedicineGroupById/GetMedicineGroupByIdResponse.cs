using Clinic.Application.Commons.Abstractions;
using System;

namespace Clinic.Application.Features.Admin.GetMedicineGroupById;

/// <summary>
///     GetMedicineGroupById Response
/// </summary>
public class GetMedicineGroupByIdResponse : IFeatureResponse
{
    public GetMedicineGroupByIdResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public MedicineGroup Group { get; init; }

        public sealed class MedicineGroup
        {
            public Guid MedicineGroupId { get; init; }
            public string Name { get; init; }
            public string Constant { get; init; }
        }
    }
}
