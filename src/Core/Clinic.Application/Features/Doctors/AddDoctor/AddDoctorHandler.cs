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
using Microsoft.IdentityModel.JsonWebTokens;

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

        // Find user by email.
        var user = await _userManager.FindByEmailAsync(request.Email);

        // Respond if user is not found.
        if (Equals(objA: user, objB: default))
        {
            return new AddDoctorResponse()
            {
                StatusCode = AddDoctorResponseStatusCode.EMAIL_DOCTOR_EXITS
            };
        }

        // Is doctor staff type id found.
        var isDoctorStaffTypeFound =
            await _unitOfWork.AddDoctorRepository.IsDoctorStaffTypeFoundByIdQueryAsync(
                doctorStaffId: Guid.Parse(request.DoctorStaffId),
                cancellationToken: cancellationToken
            );

        // Response if doctor staff type id is not found.
        if (!isDoctorStaffTypeFound)
        {
            return new()
            {
                StatusCode = AddDoctorResponseStatusCode.DOCTOR_STAFF_TYPE_GUID_IS_NOT_EXIST
            };
        }

        // Init doctor instance.
        var doctor = InitDoctorInstance(request: request);

        // Create doctor command.
        var dbResult = await _unitOfWork.AddDoctorRepository.CreateDoctorCommandAsync(
            doctor: doctor,
            roleName: "doctor",
            userPassword: "123456",
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
                Id = Id,
                Address = request.Address,
                Gender = request.Gender,
                DOB = request.DOB,
                Description = "default",
                Specialty = request.Specialty,
                Position = request.Position,
                DoctorStaffTypeId = Guid.Parse(request.DoctorStaffId)
            }
        };
    }
}
