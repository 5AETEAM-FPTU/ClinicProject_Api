using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Threading;
using System.Security.Claims;

namespace Clinic.Application.Features.Admin.DeleteMedicineById;

/// <summary>
///     DeleteMedicineById Handler
/// </summary>
public class DeleteMedicineByIdHandler
    : IFeatureHandler<DeleteMedicineByIdRequest, DeleteMedicineByIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public DeleteMedicineByIdHandler(
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
    public async Task<DeleteMedicineByIdResponse> ExecuteAsync(
        DeleteMedicineByIdRequest request,
        CancellationToken cancellationToken
    )
    {
        // Get userRole from sub type jwt
        var role = _contextAccessor.HttpContext.User.FindFirstValue(claimType: "role");

        if ((Equals(objA: role, objB: "user")))
        {
            return new() { StatusCode = DeleteMedicineByIdResponseStatusCode.FORBIDEN };
        }

        // Check medicine is exist or not
        var isMedicineExist = await _unitOfWork.DeleteMedicineByIdRepository.IsMedicineExist(
            medicineId: request.MedicineId,
            cancellationToken: cancellationToken
            );

        // Respond if medicine not exsit
        if (!isMedicineExist)
        {
            return new() { StatusCode = DeleteMedicineByIdResponseStatusCode.NOT_FOUND_MEDICINE };
        }

        // Database operation
        var dbResult = await _unitOfWork.DeleteMedicineByIdRepository.DeleteMedicineByIdCommandAsync(
            medicineId: request.MedicineId,
            cancellationToken: cancellationToken
        );

        // Respond if database operation failed.
        if (!dbResult)
        {
            return new() { StatusCode = DeleteMedicineByIdResponseStatusCode.DATABASE_OPERATION_FAIL };
        }

        // Response successfully.
        return new DeleteMedicineByIdResponse()
        {
            StatusCode = DeleteMedicineByIdResponseStatusCode.OPERATION_SUCCESS,
        };
    }

}
