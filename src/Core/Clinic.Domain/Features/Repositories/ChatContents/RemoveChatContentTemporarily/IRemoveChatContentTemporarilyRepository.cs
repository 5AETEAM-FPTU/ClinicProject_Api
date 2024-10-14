using System;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;

namespace Clinic.Domain.Features.Repositories.ChatContents.RemoveChatContentTemporarily;

/// <summary>
///     Interface for RemoveChatContentTemporarily Repository.
/// </summary>
public interface IRemoveChatContentTemporarilyRepository
{
    Task<bool> IsChatContentExistByIdQueryAsync(
        Guid chatContentId,
        CancellationToken cancellationToken = default
    );

    Task<bool> DeleteChatContentByIdCommandAsync(
        Guid chatContentId,
        Guid removedBy,
        DateTime removedAt,
        CancellationToken cancellationToken = default
    );
}
