using System;
using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.ChatContents.RemoveChatContentTemporarily;

/// <summary>
///     CreateNewOnlinePayment Response.
/// </summary>
public sealed class RemoveChatContentTemporarilyResponse : IFeatureResponse
{
    public RemoveChatContentTemporarilyResponseStatusCode StatusCode { get; init; }
}
