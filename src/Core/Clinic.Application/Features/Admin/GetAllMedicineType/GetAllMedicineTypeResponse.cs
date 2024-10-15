using Clinic.Application.Commons.Abstractions;
using System;
using System.Collections.Generic;

namespace Clinic.Application.Features.Admin.GetAllMedicineType;

/// <summary>
///     GetAllMedicineType Response
/// </summary>
public class GetAllMedicineTypeResponse : IFeatureResponse
{
    public GetAllMedicineTypeResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public  IEnumerable<MedicineType> Type { get; init; }
        public sealed class MedicineType
        {
            public Guid MedicineTypeId { get; init; }
            public string Name { get; init; }
            public string Constant { get; init; }
        }
    }
}
