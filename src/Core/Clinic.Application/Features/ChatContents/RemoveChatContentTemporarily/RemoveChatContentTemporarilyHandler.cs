using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Commons.ChatMessage.Messaging;
using Clinic.Application.Commons.Constance;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;

namespace Clinic.Application.Features.ChatContents.RemoveChatContentTemporarily;

/// <summary>
///     RemoveChatContentTemporarily Handler.
/// </summary>
internal sealed class RemoveChatContentTemporarilyHandler
    : IFeatureHandler<RemoveChatContentTemporarilyRequest, RemoveChatContentTemporarilyResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IChatHandler _chatHandler;

    public RemoveChatContentTemporarilyHandler(
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor,
        IChatHandler chatHandler
    )
    {
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
        _chatHandler = chatHandler;
    }

    public async Task<RemoveChatContentTemporarilyResponse> ExecuteAsync(
        RemoveChatContentTemporarilyRequest command,
        CancellationToken ct
    )
    {
        var userId = Guid.Parse(
            input: _httpContextAccessor.HttpContext.User.FindFirstValue(claimType: "sub")
        );

        var isChatContentExist =
            await _unitOfWork.RemoveChatContentTemporarilyRepository.IsChatContentOwnedByUserByIdQueryAsync(
                chatContentId: command.ChatContentId,
                userId: userId,
                cancellationToken: ct
            );

        if (!isChatContentExist)
        {
            return new()
            {
                StatusCode = RemoveChatContentTemporarilyResponseStatusCode.CHATCONTENT_IS_NOT_FOUND
            };
        }

        var isChatContentTemporarily =
            await _unitOfWork.RemoveChatContentTemporarilyRepository.IsChatContentTemporarilyRemovedByIdQueryAsync(
                chatContentId: command.ChatContentId,
                cancellationToken: ct
            );

        if (isChatContentTemporarily)
        {
            return new()
            {
                StatusCode =
                    RemoveChatContentTemporarilyResponseStatusCode.CHATCONTENT_IS_TEMPORARILY_REMOVED
            };
        }

        var dbResult =
            await _unitOfWork.RemoveChatContentTemporarilyRepository.DeleteChatContentByIdCommandAsync(
                chatContentId: command.ChatContentId,
                removedAt: CommonConstant.DATE_NOW_UTC,
                removedBy: userId,
                cancellationToken: ct
            );

        if (!dbResult)
        {
            return new()
            {
                StatusCode = RemoveChatContentTemporarilyResponseStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        return new()
        {
            StatusCode = RemoveChatContentTemporarilyResponseStatusCode.OPERATION_SUCCESS,
        };
    }
}
