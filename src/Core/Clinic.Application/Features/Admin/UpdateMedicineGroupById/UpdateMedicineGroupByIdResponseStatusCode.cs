namespace Clinic.Application.Features.Admin.UpdateMedicineGroupById;

/// <summary>
///     UpdateMedicineGroupById Response Status Code
/// </summary>
public enum UpdateMedicineGroupByIdResponseStatusCode
{
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    MEDICINE_GROUP_IS_NOT_FOUND,
    FORBIDEN_ACCESS
}
