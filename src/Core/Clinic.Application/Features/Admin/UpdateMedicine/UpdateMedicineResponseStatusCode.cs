namespace Clinic.Application.Features.Admin.UpdateMedicine;

/// <summary>
///     UpdateMedicine Response Status Code
/// </summary>
public enum UpdateMedicineResponseStatusCode
{
    MEDICINE_IS_NOT_FOUND,
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    MEDICINE_TYPE_ID_IS_NOT_FOUND,
    MEDICINE_GROUP_ID_IS_NOT_FOUND,
    FORBIDEN_ACCESS
}

