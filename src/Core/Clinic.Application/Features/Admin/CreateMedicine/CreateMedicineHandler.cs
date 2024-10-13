using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Commons.Constance;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Clinic.Application.Features.Admin.CreateMedicine;

internal sealed class CreateMedicineHandler
    : IFeatureHandler<CreateMedicineRequest, CreateMedicineResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public CreateMedicineHandler(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
    {
        _unitOfWork = unitOfWork;
        _contextAccessor = contextAccessor;
    }

    /// <summary>
    /// Empty implementation.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="ct"></param>
    /// <returns></returns> <summary>
    ///
    /// </summary>
    /// <param name="command"></param>
    /// <param name="ct">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///  A task containing the response.
    /// </returns>

    public async Task<CreateMedicineResponse> ExecuteAsync(
        CreateMedicineRequest command,
        CancellationToken ct
    )
    {
        //Get role
        var role = _contextAccessor.HttpContext.User.FindFirstValue(claimType: "role");

        //Check if role is not admin
        if (Equals(objA: role, objB: "user"))
        {
            return new() { StatusCode = CreateMedicineResponseStatusCode.FORBIDEN_ACCESS };
        }

        //Check if medicine already existed
        var isExisted = await _unitOfWork.CreateMedicineRepository.IsExistDrug(command.MedicineName, cancellationToken: ct);

        if (isExisted)
        {
            return new()
            {
                StatusCode = CreateMedicineResponseStatusCode.MEDICINE_ALREADY_EXISTED,
            };
        }

        // Get userId from sub type jwt
        var userId = Guid.Parse(
            _contextAccessor.HttpContext.User.FindFirstValue(claimType: JwtRegisteredClaimNames.Sub)
        );

        //Create new medicine
        var newMedicine = new Medicine()
        {
            Id = Guid.NewGuid(),
            Name = command.MedicineName,
            Ingredient = command.Ingredient,
            Manufacture = command.Manufacture,
            MedicineGroupId = command.MedicineGroupId,
            MedicineTypeId = command.MedicineTypeId,
            CreatedBy = userId,
            CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(
                dateTime: DateTime.UtcNow,
                destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(
                    id: "SE Asia Standard Time"
                )
            ),
            RemovedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
            RemovedAt = CommonConstant.MIN_DATE_TIME,
            UpdatedAt = CommonConstant.MIN_DATE_TIME,
            UpdatedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
        };

        //Create new medicine into database
        var dbResult = await _unitOfWork.CreateMedicineRepository.CreateNewMedicine(
            medicine: newMedicine,
            cancellationToken: ct
        );

        //Check if operation failed
        if (!dbResult)
        {
            return new()
            {
                StatusCode = CreateMedicineResponseStatusCode.DATABASE_OPERATION_FAIL,
            };
        }

        //Return successful code
        return new()
        {
            StatusCode = CreateMedicineResponseStatusCode.OPERATION_SUCCESS,
        };
    }
}
