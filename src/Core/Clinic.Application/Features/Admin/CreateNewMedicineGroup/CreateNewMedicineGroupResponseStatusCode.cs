namespace Clinic.Application.Features.Admin.CreateNewMedicineGroup;

public enum CreateNewMedicineGroupResponseStatusCode
{
    INPUT_VALIDATION_FAIL,
    OPERATION_SUCCESS,
    UNAUTHORIZE,
    DATABASE_OPERATION_FAIL,
    FORBIDEN_ACCESS,
    MEDICINE_GROUP_ALREADY_EXISTED
}