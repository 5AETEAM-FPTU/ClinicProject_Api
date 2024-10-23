using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Commons.Pagination;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;

namespace Clinic.Application.Features.Doctors.GetUsersHaveMedicalReport;

/// <summary>
///     GetUsersHaveMedicalReport Handler
/// </summary>
public class GetUsersHaveMedicalReportHandler
    : IFeatureHandler<GetUsersHaveMedicalReportRequest, GetUsersHaveMedicalReportResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public GetUsersHaveMedicalReportHandler(
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
    public async Task<GetUsersHaveMedicalReportResponse> ExecuteAsync(
        GetUsersHaveMedicalReportRequest request,
        CancellationToken cancellationToken
    )
    {
        // Check role "Only admin can access"
        var role = _contextAccessor.HttpContext.User.FindFirstValue(claimType: "role");
        if (!(role.Equals("staff") || role.Equals("doctor")))
        {
            return new GetUsersHaveMedicalReportResponse()
            {
                StatusCode = GetUsersHaveMedicalReportResponseStatusCode.ROLE_IS_NOT_DOCTOR_STAFF,
            };
        }

        // Get all users.
        var users =
            await _unitOfWork.GetUsersHaveMedicalReportRepository.FindUsersHaveMedicalReportsQueryAsync(
                keyword: request.Keyword,
                pageIndex: request.PageIndex,
                pageSize: request.PageSize,
                cancellationToken: cancellationToken
            );

        // Count all the users.
        var countUser =
            await _unitOfWork.GetUsersHaveMedicalReportRepository.CountAllUserQueryAsync(
                keyword: request.Keyword,
                cancellationToken: cancellationToken
            );

        // Response successfully.
        return new GetUsersHaveMedicalReportResponse()
        {
            StatusCode = GetUsersHaveMedicalReportResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new()
            {
                Users = new PaginationResponse<GetUsersHaveMedicalReportResponse.Body.User>()
                {
                    Contents = users.Select(
                        user => new GetUsersHaveMedicalReportResponse.Body.User()
                        {
                            Id = user.UserId,
                            AvatarUrl = user.User.Avatar,
                            FullName = user.User.FullName,
                            Gender = new GetUsersHaveMedicalReportResponse.Body.User.GenderDTO()
                            {
                                Id = user.User.Gender.Id,
                                Name = user.User.Gender.Name,
                                Constant = user.User.Gender.Constant,
                            },
                            Age = DateTime.Now.Year - user.DOB.Year,
                            Address = user.Address,
                        }
                    ),
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize,
                    TotalPages = (int)Math.Ceiling((double)countUser / request.PageSize),
                },
            },
        };
    }
}
