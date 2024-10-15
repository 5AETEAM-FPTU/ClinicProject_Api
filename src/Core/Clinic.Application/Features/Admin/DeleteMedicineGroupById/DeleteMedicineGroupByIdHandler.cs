using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;

namespace Clinic.Application.Features.Admin.DeleteMedicineGroupById;

/// <summary>
///     DeleteMedicineGroupById Handler
/// </summary>
public class DeleteMedicineGroupByIdHandler
    : IFeatureHandler<DeleteMedicineGroupByIdRequest, DeleteMedicineGroupByIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public DeleteMedicineGroupByIdHandler(
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
    public async Task<DeleteMedicineGroupByIdResponse> ExecuteAsync(
        DeleteMedicineGroupByIdRequest request,
        CancellationToken cancellationToken
    )
    {
        // Get userRole from sub type jwt
        var role = _contextAccessor.HttpContext.User.FindFirstValue(claimType: "role");

        if ((Equals(objA: role, objB: "user")))
        {
            return new() { StatusCode = DeleteMedicineGroupByIdResponseStatusCode.FORBIDEN };
        }

        // Check medicine group is exist or not
        var isMedicineGroupExist =
            await _unitOfWork.DeleteMedicineGroupByIdRepository.IsMedicineGroupExist(
                medicineGroupId: request.MedicineGroupId,
                cancellationToken: cancellationToken
            );

        // Respond if medicine not exsit
        if (!isMedicineGroupExist)
        {
            return new()
            {
                StatusCode = DeleteMedicineGroupByIdResponseStatusCode.NOT_FOUND_MEDICINE_GROUP,
            };
        }

        // Database operation
        var dbResult =
            await _unitOfWork.DeleteMedicineGroupByIdRepository.DeleteMedicineGroupByIdCommandAsync(
                medicineGroupId: request.MedicineGroupId,
                cancellationToken: cancellationToken
            );

        // Respond if database operation failed.
        if (!dbResult)
        {
            return new()
            {
                StatusCode = DeleteMedicineGroupByIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            };
        }

        // Response successfully.
        return new DeleteMedicineGroupByIdResponse()
        {
            StatusCode = DeleteMedicineGroupByIdResponseStatusCode.OPERATION_SUCCESS,
        };
    }
}
