using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Threading;
using System.Security.Claims;

namespace Clinic.Application.Features.Admin.UpdateMedicineTypeById;

/// <summary>
///     UpdateMedicineTypeById Handler
/// </summary>
public class UpdateMedicineTypeByIdHandler
    : IFeatureHandler<UpdateMedicineTypeByIdRequest, UpdateMedicineTypeByIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public UpdateMedicineTypeByIdHandler(
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
    public async Task<UpdateMedicineTypeByIdResponse> ExecuteAsync(
        UpdateMedicineTypeByIdRequest request,
        CancellationToken cancellationToken
    )
    {
        // Get userRole from sub type jwt
        var role = _contextAccessor.HttpContext.User.FindFirstValue(claimType: "role");

        // Response if role is not admin or staff or doctor
        if ((Equals(objA: role, objB: "user")))
        {
            return new() { StatusCode = UpdateMedicineTypeByIdResponseStatusCode.FORBIDEN_ACCESS };
        }

        // Check if medicine type not found
        var isTypeFound = await _unitOfWork.UpdateMedicineTypeByIdRepository.IsMedicineTypeFound(request.MedicineTypeId, cancellationToken);

        if (!isTypeFound)
        {
            return new() { StatusCode = UpdateMedicineTypeByIdResponseStatusCode.MEDICINE_TYPE_IS_NOT_FOUND };
        }

        // Update medicine
        var isUpdated = await _unitOfWork.UpdateMedicineTypeByIdRepository.UpdateMedicineTypeByIdQueryAsync(      
            medicineTypeId: request.MedicineTypeId,
            name: request.Name,
            constant: request.Constant,
            cancellationToken: cancellationToken
            );

        // Response if operation fail
        if (!isUpdated)
        {
            return new() { StatusCode = UpdateMedicineTypeByIdResponseStatusCode.DATABASE_OPERATION_FAIL };

        }

        // Response successfully.
        return new UpdateMedicineTypeByIdResponse()
        {
            StatusCode = UpdateMedicineTypeByIdResponseStatusCode.OPERATION_SUCCESS,
        };
    }
}
