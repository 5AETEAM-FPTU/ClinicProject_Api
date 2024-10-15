namespace Clinic.Application.Features.Admin.CreateNewMedicineType;

public enum CreateNewMedicineTypeResponseStatusCode
{
    INPUT_VALIDATION_FAIL,
    OPERATION_SUCCESS,
    UNAUTHORIZE,
    DATABASE_OPERATION_FAIL,
    FORBIDEN_ACCESS,
    MEDICINE_TYPE_ALREADY_EXISTED
}
