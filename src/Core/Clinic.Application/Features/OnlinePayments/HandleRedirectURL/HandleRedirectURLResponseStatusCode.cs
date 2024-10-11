namespace Clinic.Application.Features.OnlinePayments.HandleRedirectURL;

/// <summary>
///     GetAllDoctors Response Status Code
/// </summary>
public enum HandleRedirectURLResponseStatusCode
{
    OPERATION_SUCCESS,
    PAYMENT_IS_NOT_FOUND,
    PAYMENT_IS_ALREADY_PAID,
    DATABASE_OPERATION_FAIL,
    FORBIDDEN_ACCESS
}
