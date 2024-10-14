namespace Clinic.Application.Features.QueueRooms.CreateQueueRoom;

/// <summary>
///     CreateNewOnlinePaymentResponse StatusCode.
/// </summary>
public enum CreateQueueRoomResponseStatusCode
{
    INPUT_VALIDATION_FAIL,
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    USER_IS_NOT_FOUND,
    FORBIDEN_ACCESS,
    USER_HAVE_BEEN_ANOTHER_REQUEST_CONSULTANT
}
