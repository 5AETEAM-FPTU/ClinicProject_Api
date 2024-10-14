using Clinic.Application.Commons.Abstractions;
using FluentValidation;

namespace Clinic.Application.Features.ChatContents.RemoveChatContentTemporarily;

/// <summary>
///     RemoveChatContentTemporarily Request.
/// </summary>
public sealed class RemoveChatContentTemporarilyRequestValidator
    : FeatureRequestValidator<
        RemoveChatContentTemporarilyRequest,
        RemoveChatContentTemporarilyResponse
    >
{
    public RemoveChatContentTemporarilyRequestValidator()
    {
        RuleFor(expression: request => request.ChatContentId).NotEmpty();
    }
}
