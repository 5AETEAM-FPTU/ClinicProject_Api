using System;
using Clinic.Application.Commons.Abstractions;
using FastEndpoints;

namespace Clinic.Application.Features.ChatContents.RemoveChatContentTemporarily;

/// <summary>
///     RemoveChatContentTemporarily Request.
/// </summary>
public class RemoveChatContentTemporarilyRequest
    : IFeatureRequest<RemoveChatContentTemporarilyResponse>
{
    [BindFrom("chatContentId")]
    public Guid ChatContentId { get; set; }
}
