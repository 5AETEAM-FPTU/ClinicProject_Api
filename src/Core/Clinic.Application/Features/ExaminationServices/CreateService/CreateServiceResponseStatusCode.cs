namespace Clinic.Application.Features.ExaminationServices.CreateService;


/// <summary>
///     CreateService status code
/// </summary>
public enum CreateServiceResponseStatusCode
{
    INPUT_VALIDATION_FAIL,
    OPERATION_SUCCESS,
    UNAUTHORIZE,
    DATABASE_OPERATION_FAIL,
    FORBIDEN_ACCESS,
    SERVICE_CODE_ALREADY_EXISTED
}
