using Clinic.Application.Commons.Abstractions;
using System.Collections.Generic;
using System;

namespace Clinic.Application.Features.Admin.GetAllMedicineGroup;

/// <summary>
///     GetAllMedicineGroup Response
/// </summary>
public class GetAllMedicineGroupResponse : IFeatureResponse
{
    public GetAllMedicineGroupResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public IEnumerable<MedicineGroup> Group { get; init; }
        public sealed class MedicineGroup
        {
            public Guid MedicineGroupId { get; init; }
            public string Name { get; init; }
            public string Constant { get; init; }
        }
    }
}
