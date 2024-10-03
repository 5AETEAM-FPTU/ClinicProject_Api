using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Clinic.Application.Features.Appointments.GetAppointmentUpcoming;

/// <summary>
///     GetAppointmentUpcoming Handler
/// </summary>
public class GetAppointmentUpcomingHandler
    : IFeatureHandler<GetAppointmentUpcomingRequest, GetAppointmentUpcomingResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public GetAppointmentUpcomingHandler(
        IUnitOfWork unitOfWork,
        IHttpContextAccessor contextAccessor
    )
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
    public async Task<GetAppointmentUpcomingResponse> ExecuteAsync(
        GetAppointmentUpcomingRequest request,
        CancellationToken cancellationToken
    )
    {
        // Get userId from sub type jwt
        var userId = Guid.Parse(
            _contextAccessor.HttpContext.User.FindFirstValue(claimType: JwtRegisteredClaimNames.Sub)
        );

        // Check role doctor from role type jwt
        var role = _contextAccessor.HttpContext.User.FindFirstValue(claimType: "role");
        if (!role.Equals("user"))
        {
            return new()
            {
                StatusCode = GetAppointmentUpcomingResponseStatusCode.ROLE_IS_NOT_USER,
            };
        }

        // Found appointments date by userId.
        var dateAppointmentUpcomming =
            await _unitOfWork.GetAppointmentUpcomingRepository.GetAppointmentUpcomingByUserIdQueryAsync(
                userId: userId,
                cancellationToken: cancellationToken
            );

        // Get total of appointmented pation.
        var totalAppointmentedPation =
            await _unitOfWork.GetAppointmentUpcomingRepository.GetTotalAppointmentedByUserIdQueryAsync(
                userId,
                cancellationToken
            );

        // Response successfully.
        return new GetAppointmentUpcomingResponse()
        {
            StatusCode = GetAppointmentUpcomingResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new()
            {
                UpcomingDate = dateAppointmentUpcomming,
                TotalAppointmentedPation = totalAppointmentedPation
            },
        };
    }
}
