namespace Clinic.Application.Features.Admin.CreateMedicine;

public enum CreateMedicineResponseStatusCode
{
    INPUT_VALIDATION_FAIL,
    OPERATION_SUCCESS,
    UNAUTHORIZE,
    DATABASE_OPERATION_FAIL,
    FORBIDEN_ACCESS,
    MEDICINE_ALREADY_EXISTED
}
