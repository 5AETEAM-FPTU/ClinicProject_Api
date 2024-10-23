using Clinic.Application.Commons.Abstractions;
using FluentValidation;

namespace Clinic.Application.Features.ChatRooms.SwitchToEndChatRoom;

/// <summary>
///     SwitchToEndChatRoom Request.
/// </summary>
public sealed class SwitchToEndChatRoomRequestValidator
    : FeatureRequestValidator<SwitchToEndChatRoomRequest, SwitchToEndChatRoomResponse>
{
    public SwitchToEndChatRoomRequestValidator() { }
}
