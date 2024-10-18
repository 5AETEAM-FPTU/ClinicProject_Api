using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.Application.Features.Admin.DeleteMedicineTypeById;

/// <summary>
///     DeleteMedicineTypeById Handler
/// </summary>
public class DeleteMedicineTypeByIdHandler
    : IFeatureHandler<DeleteMedicineTypeByIdRequest, DeleteMedicineTypeByIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public DeleteMedicineTypeByIdHandler(
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
    public async Task<DeleteMedicineTypeByIdResponse> ExecuteAsync(
        DeleteMedicineTypeByIdRequest request,
        CancellationToken cancellationToken
    )
    {
        // Get userRole from sub type jwt
        var role = _contextAccessor.HttpContext.User.FindFirstValue(claimType: "role");

        if ((Equals(objA: role, objB: "user")))
        {
            return new() { StatusCode = DeleteMedicineTypeByIdResponseStatusCode.FORBIDEN };
        }

        // Check medicine type is exist or not
        var isMedicineTypeExist = await _unitOfWork.DeleteMedicineTypeByIdRepository.IsMedicineTypeExist(
            medicineTypeId: request.MedicineTypeId,
            cancellationToken: cancellationToken
            );

        // Respond if medicine not exsit
        if (!isMedicineTypeExist)
        {
            return new() { StatusCode = DeleteMedicineTypeByIdResponseStatusCode.NOT_FOUND_MEDICINE_TYPE };
        }

        // Database operation
        var dbResult = await _unitOfWork.DeleteMedicineTypeByIdRepository.DeleteMedicineTypeByIdCommandAsync(
            medicineTypeId: request.MedicineTypeId,
            cancellationToken: cancellationToken
        );

        // Respond if database operation failed.
        if (!dbResult)
        {
            return new() { StatusCode = DeleteMedicineTypeByIdResponseStatusCode.DATABASE_OPERATION_FAIL };
        }

        // Response successfully.
        return new DeleteMedicineTypeByIdResponse()
        {
            StatusCode = DeleteMedicineTypeByIdResponseStatusCode.OPERATION_SUCCESS,
        };
    }

}
