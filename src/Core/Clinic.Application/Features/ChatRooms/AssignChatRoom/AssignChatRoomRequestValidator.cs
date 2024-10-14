using Clinic.Application.Commons.Abstractions;
using FluentValidation;

namespace Clinic.Application.Features.ChatRooms.AssignChatRoom;

/// <summary>
///     CreateNewOnlinePayment Request.
/// </summary>
public sealed class AssignChatRoomRequestValidator
    : FeatureRequestValidator<AssignChatRoomRequest, AssignChatRoomResponse>
{
    public AssignChatRoomRequestValidator()
    {
        RuleFor(expression: request => request.InitialMessage).NotEmpty();
    }
}
