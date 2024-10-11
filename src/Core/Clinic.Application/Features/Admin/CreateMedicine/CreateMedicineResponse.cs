using System;
using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Admin.CreateMedicine;

/// <summary>
///     CreateMedicineResponse
/// </summary>
public sealed class CreateMedicineResponse : IFeatureResponse
{
    public CreateMedicineResponseStatusCode StatusCode { get; set; }

}

