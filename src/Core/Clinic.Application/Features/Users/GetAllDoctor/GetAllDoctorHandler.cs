using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Commons.Pagination;
using Clinic.Domain.Features.UnitOfWorks;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.Application.Features.Users.GetAllDoctor;

/// <summary>
///     GetAllDoctor Handler
/// </summary>
public class GetAllDoctorHandler : IFeatureHandler<GetAllDoctorRequest, GetAllDoctorResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllDoctorHandler(IUnitOfWork unitOfWork)
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
    public async Task<GetAllDoctorResponse> ExecuteAsync(
        GetAllDoctorRequest request,
        CancellationToken cancellationToken
    )
    {
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
                        Gender = user.Doctor.Gender,
                        DOB = user.Doctor.DOB,
                        Address = user.Doctor.Address,
                        Description = user.Doctor.Description,
                        Achievement = user.Doctor.Achievement,
                        Specialty = user.Doctor.Specialty,
                        Position = user.Doctor.Position
                    }),
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize,
                    TotalPages = (int)Math.Ceiling((double)countUser / request.PageSize)
                }
            }
        };
    }
}
