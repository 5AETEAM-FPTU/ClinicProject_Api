using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Clinic.Application.Features.Users.GetAllMedicalReports;

/// <summary>
///     GetAllMedicalReport Handler
/// </summary>
public class GetAllUserMedicalReportsHandler
    : IFeatureHandler<GetAllUserMedicalReportsRequest, GetAllUserMedicalReportsResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly UserManager<User> _userManager;

    public GetAllUserMedicalReportsHandler(
        IUnitOfWork unitOfWork,
        IHttpContextAccessor contextAccessor,
        UserManager<User> userManager
    )
    {
        _unitOfWork = unitOfWork;
        _contextAccessor = contextAccessor;
        _userManager = userManager;
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
    public async Task<GetAllUserMedicalReportsResponse> ExecuteAsync(
        GetAllUserMedicalReportsRequest request,
        CancellationToken cancellationToken
    )
    {
        // Get userId from sub type jwt
        var userId = Guid.Parse(
            _contextAccessor.HttpContext.User.FindFirstValue(claimType: JwtRegisteredClaimNames.Sub)
        );

        var foundUser = await _userManager.FindByIdAsync(userId.ToString());

        if (foundUser == null)
        {
            return new GetAllUserMedicalReportsResponse()
            {
                StatusCode = GetAllUserMedicalReportsResponseStatusCode.USER_NOT_FOUND,
            };
        }

        // Get all user medical reports.
        var reports =
            await _unitOfWork.GetAllUserMedicalReportsRepository.FindAllMedicalReportsByUserIdQueryAsync(
                request.PageIndex,
                request.PageSize,
                request.Keyword,
                userId,
                cancellationToken: cancellationToken
            );

        // Count all medical reports.
        var countService =
            await _unitOfWork.GetAllUserMedicalReportsRepository.CountAllServicesQueryAsync(
                keywork: request.Keyword,
                userId: userId,
                cancellationToken: cancellationToken
            );

        // Response successfully.
        return new GetAllUserMedicalReportsResponse()
        {
            StatusCode = GetAllUserMedicalReportsResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new GetAllUserMedicalReportsResponse.Body()
            {
                MedicalReports =
                    new Commons.Pagination.PaginationResponse<GetAllUserMedicalReportsResponse.Body.MedicalReport>
                    {
                        Contents = reports
                            .Select(
                                entity => new GetAllUserMedicalReportsResponse.Body.MedicalReport
                                {
                                    Title = entity.Appointment.Description,
                                    Id = entity.Id,
                                    ExaminedDate = entity.Appointment.ExaminationDate,
                                    Diagnosis = entity.Diagnosis,
                                    DoctorInfo =
                                        new GetAllUserMedicalReportsResponse.Body.MedicalReport.Doctor
                                        {
                                            Id = entity.Appointment.Schedule.Doctor.User.Id,
                                            FullName = entity
                                                .Appointment
                                                .Schedule
                                                .Doctor
                                                .User
                                                .FullName,
                                            AvatarUrl = entity
                                                .Appointment
                                                .Schedule
                                                .Doctor
                                                .User
                                                .Avatar,
                                        },
                                }
                            )
                            .ToList(),
                        PageIndex = request.PageIndex,
                        PageSize = request.PageSize,
                        TotalPages = (int)Math.Ceiling((double)countService / request.PageSize),
                    },
            },
        };
    }
}
