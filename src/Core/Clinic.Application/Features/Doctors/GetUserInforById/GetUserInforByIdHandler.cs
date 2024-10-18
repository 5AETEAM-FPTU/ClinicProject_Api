using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;

namespace Clinic.Application.Features.Doctors.GetUserInforById;

/// <summary>
///     GetUserInforById Handler
/// </summary>
public class GetUserInforByIdHandler
    : IFeatureHandler<GetUserInforByIdRequest, GetUserInforByIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public GetUserInforByIdHandler(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
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
    public async Task<GetUserInforByIdResponse> ExecuteAsync(
        GetUserInforByIdRequest request,
        CancellationToken cancellationToken
    )
    {
        // Get userId from sub type jwt
        var role = _contextAccessor.HttpContext.User.FindFirstValue(claimType: "role");
        if (!((role.Equals("staff")) || (role.Equals("doctor"))))
        {
            return new GetUserInforByIdResponse()
            {
                StatusCode = GetUserInforByIdResponseStatusCode.FORBIDDEN,
            };
        }

        // Found user by userId
        var foundUser = await _unitOfWork.GetUserInforByIdRepository.GetUserByUserIdQueryAsync(
            userId: request.UserId,
            cancellationToken: cancellationToken
        );

        // Responds if userId is not found
        if (Equals(objA: foundUser, objB: default))
        {
            return new GetUserInforByIdResponse()
            {
                StatusCode = GetUserInforByIdResponseStatusCode.USER_IS_NOT_FOUND,
            };
        }

        // Response successfully.
        return new GetUserInforByIdResponse()
        {
            StatusCode = GetUserInforByIdResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new()
            {
                User = new()
                {
                    AvatarUrl = foundUser.Avatar,
                    FullName = foundUser.FullName,
                    Gender = new() { Id = foundUser.Gender.Id, GenderName = foundUser.Gender.Name },
                    Age = DateTime.Now.Year - foundUser.Patient.DOB.Year ,
                    Address = foundUser.Patient.Address,
                    Description = foundUser.Patient.Description,
                    UserId = foundUser.Id,
                },
            },
        };
    }
}
