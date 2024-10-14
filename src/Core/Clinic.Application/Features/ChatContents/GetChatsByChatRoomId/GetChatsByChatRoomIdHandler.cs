using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Commons.Constance;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Clinic.Application.Features.ChatContents.GetChatsByChatRoomId;

/// <summary>
///     GetChatsByChatRoomId Handler
/// </summary>
public class GetChatsByChatRoomIdHandler
    : IFeatureHandler<GetChatsByChatRoomIdRequest, GetChatsByChatRoomIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public GetChatsByChatRoomIdHandler(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
    {
        _unitOfWork = unitOfWork;
        _contextAccessor = contextAccessor;
    }

    /// <summary>
    ///     Entry of new request handler.
    /// </summary>
    /// <param name="request">
    ///     Request model.
    /// </param>
    /// <param name="ct">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     A task containing the response.
    public async Task<GetChatsByChatRoomIdResponse> ExecuteAsync(
        GetChatsByChatRoomIdRequest request,
        CancellationToken cancellationToken
    )
    {
        // Get userId from sub type jwt
        var userId = Guid.Parse(
            _contextAccessor.HttpContext.User.FindFirstValue(claimType: JwtRegisteredClaimNames.Sub)
        );

        var isUserOwneOfChatRoom =
            await _unitOfWork.GetChatsByChatRoomIdRepository.IsUserOwnerOfChatRoomQueryAsync(
                chatRoomId: request.ChatRoomId,
                userId: userId,
                cancellationToken: cancellationToken
            );

        if (!isUserOwneOfChatRoom)
        {
            return new()
            {
                StatusCode = GetChatsByChatRoomIdResponseStatusCode.USER_CAN_ACCESS_THIS_CHAT
            };
        }

        var chatContents =
            await _unitOfWork.GetChatsByChatRoomIdRepository.FindChatContentsByChatRoomIdQueryAsync(
                chatRoomId: request.ChatRoomId,
                lastTimeOfBefore: request.LastReportDate,
                limit: request.PageSize,
                cancellationToken: cancellationToken
            );

        // Response successfully.
        return new GetChatsByChatRoomIdResponse()
        {
            StatusCode = GetChatsByChatRoomIdResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new GetChatsByChatRoomIdResponse.Body()
            {
                Messages = chatContents.Select(
                    entity => new GetChatsByChatRoomIdResponse.Body.Message()
                    {
                        ChatContentId = entity.Id,
                        SenderId = entity.SenderId,
                        Content = entity.TextContent,
                        Time = entity.CreatedAt,
                        IsRemoved = entity.RemovedBy != CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
                        AssetUrl = entity.Assets.Select(asset => asset.FilePath)
                    }
                )
            }
        };
    }
}
