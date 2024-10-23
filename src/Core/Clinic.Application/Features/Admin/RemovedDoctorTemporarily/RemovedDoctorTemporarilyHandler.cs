using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;

namespace Clinic.Application.Features.Admin.RemovedDoctorTemporarily;

/// <summary>
///     RemovedDoctorTemporarily Handler
/// </summary>
public class RemovedDoctorTemporarilyHandler
    : IFeatureHandler<RemovedDoctorTemporarilyRequest, RemovedDoctorTemporarilyResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public RemovedDoctorTemporarilyHandler(
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
    public async Task<RemovedDoctorTemporarilyResponse> ExecuteAsync(
        RemovedDoctorTemporarilyRequest request,
        CancellationToken cancellationToken
    )
    {
        // Get userRole from sub type jwt
        var userId = _contextAccessor.HttpContext.User.FindFirstValue(claimType: "sub");

        // Get userRole from sub type jwt
        var role = _contextAccessor.HttpContext.User.FindFirstValue(claimType: "role");

        if (!(Equals(objA: role, objB: "admin")))
        {
            return new() { StatusCode = RemovedDoctorTemporarilyResponseStatusCode.FORBIDEN };
        }

        // Check doctor is exist or not
        var doctor =
            await _unitOfWork.RemovedDoctorTemporarilyRepository.IsDoctorFoundByIdQueryAsync(
                doctorId: request.DoctorId,
                cancellationToken: cancellationToken
            );

        // Respond if doctor not exsit
        if (!doctor)
        {
            return new()
            {
                StatusCode = RemovedDoctorTemporarilyResponseStatusCode.DOCTOR_NOT_EXIST
            };
        }

        // Database operation
        var dbResult =
            await _unitOfWork.RemovedDoctorTemporarilyRepository.DeleteDoctorTemporarilyByIdCommandAsync(
                doctorId: request.DoctorId,
                adminId: Guid.Parse(userId),
                cancellationToken: cancellationToken
            );

        // Respond if database operation failed.
        if (!dbResult)
        {
            return new()
            {
                StatusCode = RemovedDoctorTemporarilyResponseStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        // Response successfully.
        return new RemovedDoctorTemporarilyResponse()
        {
            StatusCode = RemovedDoctorTemporarilyResponseStatusCode.OPERATION_SUCCESS,
        };
    }
}
