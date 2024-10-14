namespace Clinic.Application.Features.Admin.DeleteMedicineTypeById;

/// <summary>
///     DeleteMedicineTypeById Response Status Code
/// </summary>
public enum DeleteMedicineTypeByIdResponseStatusCode
{
    DATABASE_OPERATION_FAIL,
    FORBIDEN,
    OPERATION_SUCCESS,
    NOT_FOUND_MEDICINE_TYPE
}
