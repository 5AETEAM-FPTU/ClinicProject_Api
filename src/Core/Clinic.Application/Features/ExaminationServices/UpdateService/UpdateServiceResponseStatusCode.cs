namespace Clinic.Application.Features.ExaminationServices.UpdateService;


/// <summary>
///     UpdateService status code
/// </summary>
public enum UpdateServiceResponseStatusCode
{
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    ROLE_IS_NOT_ADMIN_STAFF,
    SERVICE_NOT_FOUND,
    SERVICE_CODE_ALREADY_EXISTED
}
