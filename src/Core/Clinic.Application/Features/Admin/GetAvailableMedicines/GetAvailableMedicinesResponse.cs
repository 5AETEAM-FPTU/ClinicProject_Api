using System;
using System.Collections.Generic;
using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Admin.GetAvailableMedicines;

/// <summary>
///     GetAvailableMedicines Response
/// </summary>
public class GetAvailableMedicinesResponse : IFeatureResponse
{
    public GetAvailableMedicinesResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public IEnumerable<Medicine> Medicines { get; init; }

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
                public string Constant { get; init; }
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
