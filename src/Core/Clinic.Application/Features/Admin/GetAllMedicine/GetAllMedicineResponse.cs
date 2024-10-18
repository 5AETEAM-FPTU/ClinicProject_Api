using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Commons.Pagination;
using System;

namespace Clinic.Application.Features.Admin.GetAllMedicine;

/// <summary>
///     GetAllMedicine Response
/// </summary>
public class GetAllMedicineResponse : IFeatureResponse
{
    public GetAllMedicineResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public PaginationResponse<Medicine> Medicines { get; init; }

        public sealed class Medicine
        {
            public Guid MedicineId { get; init; }
            public string MedicineName { get; init; }
            public string Manufacture { get; init; }
            public string Ingredient { get; init; }
            public MedicineType Type { get; init; }
            public MedicineGroup Group { get; init; }

            public sealed class MedicineType
            {
                public Guid TypeId { get; init; }
                public string Name { get; init; }
                public string Constant {  get; init; }
            }

            public sealed class MedicineGroup
            {
                public Guid GroupId { get; init; }
                public string Name { get; init; }
                public string Constant { get; init; }
            }
        }
    }
}
