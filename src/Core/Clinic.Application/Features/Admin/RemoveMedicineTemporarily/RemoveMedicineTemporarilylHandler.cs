using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;

namespace Clinic.Application.Features.Admin.RemoveMedicineTemporarily;

/// <summary>
///     RemoveMedicineTemporarily Handler
/// </summary>
public class RemoveMedicineTemporarilyHandler
    : IFeatureHandler<RemoveMedicineTemporarilyRequest, RemoveMedicineTemporarilyResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public RemoveMedicineTemporarilyHandler(
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
    public async Task<RemoveMedicineTemporarilyResponse> ExecuteAsync(
        RemoveMedicineTemporarilyRequest request,
        CancellationToken cancellationToken
    )
    {
        // Get userRole from sub type jwt
        var userId = _contextAccessor.HttpContext.User.FindFirstValue(claimType: "sub");

        // Get userRole from sub type jwt
        var role = _contextAccessor.HttpContext.User.FindFirstValue(claimType: "role");

        if ((Equals(objA: role, objB: "user")))
        {
            return new() { StatusCode = RemoveMedicineTemporarilyResponseStatusCode.FORBIDEN };
        }

        // Check medicine is exist or not
        var medicine = await _unitOfWork.GetMedicineByIdRepository.FindMedicineByIdQueryAsync(
            medicineId: request.MedicineId,
            cancellationToken: cancellationToken
        );

        // Respond if medicine not exsit
        if (Equals(medicine, default))
        {
            return new() { StatusCode = RemoveMedicineTemporarilyResponseStatusCode.NOT_FOUND_MEDICINE };
        }

        // Handler remove temporarily
        medicine.RemovedAt = DateTime.Now;
        medicine.RemovedBy = Guid.Parse(userId);

        // Database operation
        var dbResult = await _unitOfWork.RemoveMedicineTemporarilyRepository.RemoveMedicineTemporarilyByIdCommandAsync(
            medicine: medicine,
            cancellationToken: cancellationToken
        );

        // Respond if database operation failed.
        if (!dbResult)
        {
            return new() { StatusCode = RemoveMedicineTemporarilyResponseStatusCode.DATABASE_OPERATION_FAIL };
        }

        // Response successfully.
        return new RemoveMedicineTemporarilyResponse()
        {
            StatusCode = RemoveMedicineTemporarilyResponseStatusCode.OPERATION_SUCCESS,
        };
    }
}
