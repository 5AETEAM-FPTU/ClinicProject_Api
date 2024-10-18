using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Commons.Pagination;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;

namespace Clinic.Application.Features.Admin.GetAllUser;

/// <summary>
///     GetAllUser Handler
/// </summary>
public class GetAllUserHandler : IFeatureHandler<GetAllUserRequest, GetAllUserResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public GetAllUserHandler(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
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
    public async Task<GetAllUserResponse> ExecuteAsync(
        GetAllUserRequest request,
        CancellationToken cancellationToken
    )
    {
        // Check role "Only admin can access"
        var role = _contextAccessor.HttpContext.User.FindFirstValue(claimType: "role");
        if (!role.Equals("admin"))
        {
            return new GetAllUserResponse()
            {
                StatusCode = GetAllUserResponseStatusCode.ROLE_IS_NOT_ADMIN,
            };
        }

        // Get all users.
        var users = await _unitOfWork.GetAllUsersRepository.FindUserByIdQueryAsync(
            pageIndex: request.PageIndex,
            pageSize: request.PageSize,
            cancellationToken: cancellationToken
        );

        // Count all the users.
        var countUser = await _unitOfWork.GetAllUsersRepository.CountAllUserQueryAsync(
            cancellationToken: cancellationToken
        );

        // Response successfully.
        return new GetAllUserResponse()
        {
            StatusCode = GetAllUserResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new()
            {
                Users = new PaginationResponse<GetAllUserResponse.Body.User>()
                {
                    Contents = users.Select(user => new GetAllUserResponse.Body.User()
                    {
                        Id = user.Id,
                        Username = user.UserName,
                        PhoneNumber = user.PhoneNumber,
                        AvatarUrl = user.Avatar,
                        FullName = user.FullName,
                        Gender = new GetAllUserResponse.Body.User.GenderDTO()
                        {
                            Id = user.Gender.Id,
                            Name = user.Gender.Name,
                            Constant = user.Gender.Constant,
                        },
                        DOB = user.Patient.DOB,
                        Address = user.Patient.Address,
                        Description = user.Patient.Description,
                    }),
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize,
                    TotalPages = (int)Math.Ceiling((double)countUser / request.PageSize),
                },
            },
        };
    }
}
