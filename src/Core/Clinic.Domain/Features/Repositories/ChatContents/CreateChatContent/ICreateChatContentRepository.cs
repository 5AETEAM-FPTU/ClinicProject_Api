using System;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;

namespace Clinic.Domain.Features.Repositories.ChatContents.CreateChatContent;

/// <summary>
///     Interface for CreateChatContent Repository.
/// </summary>
public interface ICreateChatContentRepository
{
    Task<bool> AddChatContentCommandAsync(
        ChatContent chatContent,
        DateTime createdTime,
        Guid chatRoomId,
        CancellationToken cancellationToken = default
    );

    Task<bool> IsChatRoomExperiedQueryAsync(
        Guid chatRoomId,
        CancellationToken cancellationToken = default
    );
}
