using Clinic.Application.Commons.Abstractions;
using System;

namespace Clinic.Application.Features.Admin.GetMedicineTypeById;

/// <summary>
///     GetMedicineTypeById Response
/// </summary>
public class GetMedicineTypeByIdResponse : IFeatureResponse
{
    public GetMedicineTypeByIdResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public MedicineType Type { get; init; }

        public sealed class MedicineType
        {
            public Guid MedicineTypeId { get; init; }
            public string Name { get; init; }
            public string Constant { get; init; }
        }
    }
}
