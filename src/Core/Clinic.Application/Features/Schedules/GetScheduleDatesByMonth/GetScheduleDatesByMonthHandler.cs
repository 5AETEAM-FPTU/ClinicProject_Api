using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Clinic.Application.Features.Schedules.GetScheduleDatesByMonth;

/// <summary>
///     GetScheduleDatesByMonth Handler
/// </summary>
public class GetScheduleDatesByMonthHandler
    : IFeatureHandler<GetScheduleDatesByMonthRequest, GetScheduleDatesByMonthResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public GetScheduleDatesByMonthHandler(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
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
    public async Task<GetScheduleDatesByMonthResponse> ExecuteAsync(
        GetScheduleDatesByMonthRequest request,
        CancellationToken cancellationToken
    )
    {

        // Get userId from sub type jwt
        var userId = Guid.Parse(
            _contextAccessor.HttpContext.User.FindFirstValue(claimType: JwtRegisteredClaimNames.Sub)
        );

        // Found user by userId
        var foundUser =
            await _unitOfWork.GetScheduleDatesByMonthRepository.GetUserByIdAsync(
                    userId,
                    cancellationToken
                );

        // Responds if userId is not found
        if (Equals(objA: foundUser, objB: default))
        {
            return new GetScheduleDatesByMonthResponse()
            {
                StatusCode = GetScheduleDatesByMonthResponseStatusCode.USER_IS_NOT_FOUND
            };
        }

        // Get Schedule dates
        var schedules = await _unitOfWork.GetScheduleDatesByMonthRepository.GetScheduleDatesByMonthQueryAsync(
            year: request.Year,
            month: request.Month,
            userId: userId,
            cancellationToken: cancellationToken
        );

        // Response successfully.
        return new GetScheduleDatesByMonthResponse()
        {
            StatusCode = GetScheduleDatesByMonthResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new()
            {
                WorkingDays = schedules.Select(s => s.StartDate.Date).Distinct().ToList()
            }
        };
    }
}
