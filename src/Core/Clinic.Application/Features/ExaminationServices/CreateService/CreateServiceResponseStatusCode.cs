namespace Clinic.Application.Features.ExaminationServices.CreateService;


/// <summary>
///     CreateService status code
/// </summary>
public enum CreateServiceResponseStatusCode
{
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    ROLE_IS_NOT_ADMIN_STAFF,
    SERVICE_CODE_ALREADY_EXISTED
}
