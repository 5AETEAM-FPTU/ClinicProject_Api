﻿using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Commons.Pagination;
using Clinic.Domain.Features.UnitOfWorks;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.Application.Features.Users.GetAllUser;

/// <summary>
///     GetAllDoctor Handler
/// </summary>
public class GetAllUserHandler : IFeatureHandler<GetAllUserRequest, GetAllUserResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllUserHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
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
        // Get all users.
        var users =
            await _unitOfWork.GetAllUsersRepository.FindUserByIdQueryAsync(
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
                    Contents = users.Select(patient => new GetAllUserResponse.Body.User()
                    {
                        Id = patient.Id,
                        Username = patient.User.UserName,
                        PhoneNumber = patient.User.PhoneNumber,
                        AvatarUrl = patient.User.Avatar,
                        FullName = patient.User.FullName,
                        Gender = patient.Gender,
                        DOB = patient.DOB,
                        Address = patient.Address,
                        Description = patient.Description,
                    }),
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize,
                    TotalPages = (int)Math.Ceiling((double)countUser / request.PageSize)
                }
            }
        };
    }
}
