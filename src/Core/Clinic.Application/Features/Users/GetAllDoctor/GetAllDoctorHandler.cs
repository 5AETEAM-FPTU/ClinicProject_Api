using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Commons.Pagination;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.Application.Features.Users.GetAllDoctor;

/// <summary>
///     GetAllDoctor Handler
/// </summary>
public class GetAllDoctorHandler : IFeatureHandler<GetAllDoctorRequest, GetAllDoctorResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;
    public GetAllDoctorHandler(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
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
    public async Task<GetAllDoctorResponse> ExecuteAsync(
        GetAllDoctorRequest request,
        CancellationToken cancellationToken
    )
    {
        // Get userId from sub type jwt
        var role = _contextAccessor.HttpContext.User.FindFirstValue(claimType: "role");
        if (!role.Equals("admin"))
        {
            return new GetAllDoctorResponse()
            {
                StatusCode = GetAllDoctorResponseStatusCode.ROLE_IS_NOT_ADMIN
            };
        }

        // Get all users.
        var users =
            await _unitOfWork.GetAllDoctorRepository.FindAllDoctorsQueryAsync(
                pageIndex: request.PageIndex,
                pageSize: request.PageSize,
                cancellationToken: cancellationToken
            );

        // Count all the users.
        var countUser = await _unitOfWork.GetAllDoctorRepository.CountAllDoctorsQueryAsync(
            cancellationToken: cancellationToken
        );

        // Response successfully.
        return new GetAllDoctorResponse()
        {
            StatusCode = GetAllDoctorResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new()
            {
                Users = new PaginationResponse<GetAllDoctorResponse.Body.User>()
                {
                    Contents = users.Select(user => new GetAllDoctorResponse.Body.User()
                    {
                        Id = user.Id,
                        Username = user.UserName,
                        PhoneNumber = user.PhoneNumber,
                        AvatarUrl = user.Avatar,
                        FullName = user.FullName,
                        //Gender = user.Doctor.Gender,
                        DOB = user.Doctor.DOB,
                        Address = user.Doctor.Address,
                        Description = user.Doctor.Description,
                        Achievement = user.Doctor.Achievement,
                        //Specialty = user.Doctor.Specialty,
                        //Position = user.Doctor.Position
                    }),
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize,
                    TotalPages = (int)Math.Ceiling((double)countUser / request.PageSize)
                }
            }
        };
    }
}
