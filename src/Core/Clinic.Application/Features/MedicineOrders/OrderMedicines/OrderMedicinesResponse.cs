using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.MedicineOrders.OrderMedicines;

/// <summary>
///     GetMedicineOrderItems Response
/// </summary>
public class OrderMedicinesResponse : IFeatureResponse
{
    public OrderMedicinesResponseStatusCode StatusCode { get; init; }
}
