namespace Clinic.Application.Features.Admin.UpdateMedicineTypeById;

/// <summary>
///     UpdateMedicineTypeById Response Status Code
/// </summary>
public enum UpdateMedicineTypeByIdResponseStatusCode
{
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    MEDICINE_TYPE_IS_NOT_FOUND,
    FORBIDEN_ACCESS
}
