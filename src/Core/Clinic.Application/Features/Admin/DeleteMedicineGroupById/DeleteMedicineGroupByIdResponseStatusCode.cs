namespace Clinic.Application.Features.Admin.DeleteMedicineGroupById;

/// <summary>
///     DeleteMedicineGroupById Response Status Code
/// </summary>
public enum DeleteMedicineGroupByIdResponseStatusCode
{
    DATABASE_OPERATION_FAIL,
    FORBIDEN,
    OPERATION_SUCCESS,
    NOT_FOUND_MEDICINE_GROUP
}
