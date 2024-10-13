namespace Clinic.Application.Features.Admin.DeleteMedicineById;

/// <summary>
///     DeleteMedicineById Response Status Code
/// </summary>
public enum DeleteMedicineByIdResponseStatusCode
{
    DATABASE_OPERATION_FAIL,
    FORBIDEN,
    OPERATION_SUCCESS,
    NOT_FOUND_MEDICINE
}
