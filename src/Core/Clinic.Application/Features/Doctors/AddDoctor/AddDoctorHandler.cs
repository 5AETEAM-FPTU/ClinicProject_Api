using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Commons.Constance;
using Clinic.Application.Commons.FIleObjectStorage;
using Clinic.Application.Features.Doctors.AddDoctor;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Clinic.Application.Features.Auths.AddDoctor;

/// <summary>
///     GetAllDoctor Handler
/// </summary>
public class AddDoctorHandler : IFeatureHandler<AddDoctorRequest, AddDoctorResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;
    private IDefaultUserAvatarAsUrlHandler _defaultUserAvatarAsUrlHandler;
    private readonly IHttpContextAccessor _contextAccessor;

    public AddDoctorHandler(
        IUnitOfWork unitOfWork,
        UserManager<User> userManager,
        IDefaultUserAvatarAsUrlHandler defaultUserAvatarAsUrlHandlerl,
        IHttpContextAccessor contextAccessor
    )
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _defaultUserAvatarAsUrlHandler = defaultUserAvatarAsUrlHandlerl;
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
    public async Task<AddDoctorResponse> ExecuteAsync(
        AddDoctorRequest request,
        CancellationToken cancellationToken
    )
    {
        // Get userId from sub type jwt
        var role = _contextAccessor.HttpContext.User.FindFirstValue(claimType: "role");

        if (!Equals(objA: role, objB: "admin"))
        {
            return new() { StatusCode = AddDoctorResponseStatusCode.FORBIDEN_ACCESS };
        }

        // Is genderId found
        var isGenderFound = await _unitOfWork.AddDoctorRepository.IsGenderFoundByIdQueryAsync(
            genderId: request.GenderId,
            cancellationToken: cancellationToken
        );

        // Respond if genderId is not found
        if (!isGenderFound)
        {
            return new() { StatusCode = AddDoctorResponseStatusCode.GENDER_ID_IS_NOT_FOUND };
        }

        // Is positionId found
        var positionIdFound = await _unitOfWork.AddDoctorRepository.IsPositionFoundByIdQueryAsync(
            positionId: request.PositionId,
            cancellationToken: cancellationToken
        );

        // Respond if genderId is not found
        if (!positionIdFound)
        {
            return new() { StatusCode = AddDoctorResponseStatusCode.POSITION_ID_IS_NOT_FOUND };
        }

        // Is specialtyId found
        var isSpecialtFound = await _unitOfWork.AddDoctorRepository.IsSpecialtyFoundByIdQueryAsync(
            specialtyId: request.SpecialtyId,
            cancellationToken: cancellationToken
        );

        // Respond if genderId is not found
        if (!isSpecialtFound)
        {
            return new() { StatusCode = AddDoctorResponseStatusCode.SPECIALTY_ID_IS_NOT_FOUND };
        }

        // Find user by email.
        var user = await _userManager.FindByEmailAsync(request.Email);

        // Respond if user is not found.
        if (!Equals(objA: user, objB: default))
        {
            return new AddDoctorResponse()
            {
                StatusCode = AddDoctorResponseStatusCode.EMAIL_DOCTOR_EXITS
            };
        }

        // Init doctor instance.
        var doctor = InitDoctorInstance(request: request);

        // Create doctor command.
        var dbResult = await _unitOfWork.AddDoctorRepository.CreateDoctorCommandAsync(
            doctor: doctor,
            roleName: "doctor",
            userPassword: "Admin123@",
            cancellationToken: cancellationToken
        );

        // Response if database operation failed.
        if (!dbResult)
        {
            return new() { StatusCode = AddDoctorResponseStatusCode.DATABASE_OPERATION_FAIL };
        }

        // Response successfully.
        return new AddDoctorResponse()
        {
            StatusCode = AddDoctorResponseStatusCode.OPERATION_SUCCESS,
        };
    }

    private User InitDoctorInstance(AddDoctorRequest request)
    {
        var Id = Guid.NewGuid();

        return new()
        {
            Id = Id,
            FullName = request.FullName,
            UserName = request.Email,
            Email = request.Email,
            Avatar = _defaultUserAvatarAsUrlHandler.Get(),
            PhoneNumber = request.PhoneNumber,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
            UpdatedAt = CommonConstant.MIN_DATE_TIME,
            UpdatedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
            RemovedAt = CommonConstant.MIN_DATE_TIME,
            RemovedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
            Doctor = new()
            {
                DOB = request.DOB,
                Description = "default",
                Achievement = "default"
            }
        };
    }
}
