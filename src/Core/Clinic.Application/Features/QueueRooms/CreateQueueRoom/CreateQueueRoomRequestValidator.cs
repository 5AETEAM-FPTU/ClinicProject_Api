using Clinic.Application.Commons.Abstractions;
using FluentValidation;

namespace Clinic.Application.Features.QueueRooms.CreateQueueRoom;

/// <summary>
///     CreateQueueRoom Request.
/// </summary>
public sealed class CreateQueueRoomRequestValidator
    : FeatureRequestValidator<CreateQueueRoomRequest, CreateQueueRoomResponse>
{
    public CreateQueueRoomRequestValidator()
    {
        RuleFor(expression: request => request.Title).MaximumLength(maximumLength: 128).NotEmpty();

        RuleFor(expression: request => request.Message)
            .MaximumLength(maximumLength: 500)
            .NotEmpty();
    }
}
