using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Security.Claims;
using System.Linq;

namespace Clinic.Application.Features.Users.GetConsultationOverview;

/// <summary>
///     GetConsultationOverview Handler
/// </summary>
public class GetConsultationOverviewHandler : IFeatureHandler<GetConsultationOverviewRequest, GetConsultationOverviewResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public GetConsultationOverviewHandler(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
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
    public async Task<GetConsultationOverviewResponse> ExecuteAsync(
        GetConsultationOverviewRequest request,
        CancellationToken cancellationToken
    )
    {
        // Get userId from sub type jwt
        var userId = Guid.Parse(
            _contextAccessor.HttpContext.User.FindFirstValue(claimType: JwtRegisteredClaimNames.Sub)
        );

        // Check role "Only user can access"
        var role = _contextAccessor.HttpContext.User.FindFirstValue(claimType: "role");
        if (!role.Equals("user"))
        {
            return new GetConsultationOverviewResponse()
            {
                StatusCode = GetConsultationOverviewResponseStatusCode.ROLE_IS_NOT_USER
            };
        }

        // Get all pending consultations.
        var pendings = await _unitOfWork.GetConsultationOverviewRepository.FindAllPendingConsultationByUserIdQueryAsync(
            userId,
            cancellationToken: cancellationToken
        );

        // Get all done consultation.
        var dones = await _unitOfWork.GetConsultationOverviewRepository.FindAllDoneConsultationByUserIdQueryAsync(
            userId,
            cancellationToken: cancellationToken
            );

        // Response successfully.
        return new GetConsultationOverviewResponse()
        {
            StatusCode = GetConsultationOverviewResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new GetConsultationOverviewResponse.Body()
            {
                PendingConsultations = pendings.Select(pending => new GetConsultationOverviewResponse.Body.PendingConsultation()
                {
                    PendingConsultationId = pending.Id,
                    Content = pending.Message,
                    Title = pending.Title,
                }),
                DoneConsultations = dones.Select(done => new GetConsultationOverviewResponse.Body.DoneConsultation()
                {
                    DoneConsultationId  = done.Id,
                    Content = done.LastMessage,
                    DoctorName = done.Doctor.User.FullName,
                    Title = pendings
                            .Where(pending => pending.PatientId == done.PatientId)
                            .Select(pending => pending.Title)
                            .FirstOrDefault()
                            .ToString()
                })
            }
        };
    }
}
