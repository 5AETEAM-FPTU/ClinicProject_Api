using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Threading.Tasks;
using System.Threading;
using System;
using Clinic.Application.Features.Schedules.UpdateSchedule;
using System.Security.Claims;

namespace Clinic.Application.Features.Admin.UpdateMedicine;

/// <summary>
///     UpdateMedicine Handler
/// </summary>
public class UpdateMedicineHandler
    : IFeatureHandler<UpdateMedicineRequest, UpdateMedicineResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public UpdateMedicineHandler(
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
    public async Task<UpdateMedicineResponse> ExecuteAsync(
        UpdateMedicineRequest request,
        CancellationToken cancellationToken
    )
    {
        // Get userRole from sub type jwt
        var role = _contextAccessor.HttpContext.User.FindFirstValue(claimType: "role");

        // Response if role is not admin or staff or doctor
        if ((Equals(objA: role, objB: "user")))
        {
            return new() { StatusCode = UpdateMedicineResponseStatusCode.FORBIDEN_ACCESS };
        }

        // Check if medicine group not found
        var isGroupFound = await _unitOfWork.UpdateMedicineRepository.IsMedicineGroupFound(request.MedicineGroupId, cancellationToken);

        if (!isGroupFound)
        {
            return new() { StatusCode = UpdateMedicineResponseStatusCode.MEDICINE_GROUP_ID_IS_NOT_FOUND };
        }

        // Check if medicine type not found
        var isTypeFound = await _unitOfWork.UpdateMedicineRepository.IsMedicineTypeFound(request.MedicineTypeId, cancellationToken);

        if (!isTypeFound)
        {
            return new() { StatusCode = UpdateMedicineResponseStatusCode.MEDICINE_TYPE_ID_IS_NOT_FOUND };
        }

        // Check if medicine not found
        var isMedicineFound = await _unitOfWork.UpdateMedicineRepository.IsMedicineFoundById(request.MedicineId, cancellationToken);

        if (!isMedicineFound)
        {
            return new() { StatusCode = UpdateMedicineResponseStatusCode.MEDICINE_IS_NOT_FOUND };
        }

        // Update medicine
        var isUpdated = await _unitOfWork.UpdateMedicineRepository.UpdateMedicineQueryAsync(
            medicineId: request.MedicineId,
            medicineName: request.MedicineName,
            manufacture: request.Manufacture,
            medicineGroupId: request.MedicineGroupId,
            ingredient: request.Ingredient,
            medicineTypeId: request.MedicineTypeId,
            cancellationToken: cancellationToken
            );

        // Response if operation fail
        if( !isUpdated )
        {
            return new() { StatusCode = UpdateMedicineResponseStatusCode.DATABASE_OPERATION_FAIL };

        }

        // Response successfully.
        return new UpdateMedicineResponse()
        {
            StatusCode = UpdateMedicineResponseStatusCode.OPERATION_SUCCESS,
        };
    }
}
