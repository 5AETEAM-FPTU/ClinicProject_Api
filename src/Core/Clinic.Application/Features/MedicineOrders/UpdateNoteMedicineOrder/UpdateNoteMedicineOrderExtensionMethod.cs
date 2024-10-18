
namespace Clinic.Application.Features.MedicineOrders.UpdateNoteMedicineOrder;

/// <summary>
///     Extension Method for UpdateMedicineOrderItem features.
/// </summary>
public static class UpdateNoteMedicineOrderExtensionMethod
{
    public static string ToAppCode(this UpdateNoteMedicineOrderResponseStatusCode statusCode)
    {
        return $"{nameof(UpdateNoteMedicineOrder)}Feature: {statusCode}";
    }
}
