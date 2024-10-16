using System;
using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.MedicineOrders.OrderMedicines;

/// <summary>
///     GetMedicineOrderItems Request
/// </summary>

public class OrderMedicinesRequest : IFeatureRequest<OrderMedicinesResponse>
{
    public Guid MedicineOrderId { get; init; }
    public Guid MedicineId { get; init; }
}
