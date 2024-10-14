using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;

namespace Clinic.Application.Features.Admin.UpdateMedicineGroupById;

/// <summary>
///     UpdateMedicineGroupById Handler
/// </summary>
public class UpdateMedicineGroupByIdHandler
    : IFeatureHandler<UpdateMedicineGroupByIdRequest, UpdateMedicineGroupByIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public UpdateMedicineGroupByIdHandler(
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
    public async Task<UpdateMedicineGroupByIdResponse> ExecuteAsync(
        UpdateMedicineGroupByIdRequest request,
        CancellationToken cancellationToken
    )
    {
        // Get userRole from sub type jwt
        var role = _contextAccessor.HttpContext.User.FindFirstValue(claimType: "role");

        // Response if role is not admin or staff or doctor
        if ((Equals(objA: role, objB: "user")))
        {
            return new() { StatusCode = UpdateMedicineGroupByIdResponseStatusCode.FORBIDEN_ACCESS };
        }

        // Check if medicine group not found
        var isGroupFound = await _unitOfWork.UpdateMedicineGroupByIdRepository.IsMedicineGroupFound(
            request.MedicineGroupId,
            cancellationToken
        );

        if (!isGroupFound)
        {
            return new()
            {
                StatusCode = UpdateMedicineGroupByIdResponseStatusCode.MEDICINE_GROUP_IS_NOT_FOUND,
            };
        }

        // Update medicine
        var isUpdated =
            await _unitOfWork.UpdateMedicineGroupByIdRepository.UpdateMedicineGroupByIdQueryAsync(
                medicineGroupId: request.MedicineGroupId,
                name: request.Name,
                constant: request.Constant,
                cancellationToken: cancellationToken
            );

        // Response if operation fail
        if (!isUpdated)
        {
            return new()
            {
                StatusCode = UpdateMedicineGroupByIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            };
        }

        // Response successfully.
        return new UpdateMedicineGroupByIdResponse()
        {
            StatusCode = UpdateMedicineGroupByIdResponseStatusCode.OPERATION_SUCCESS,
        };
    }
}
