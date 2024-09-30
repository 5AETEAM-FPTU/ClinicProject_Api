using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;

namespace Clinic.Application.Features.Appointments.CreateNewAppointment;

internal sealed class CreateNewAppointmentHandler
    : IFeatureHandler<CreateNewAppointmentRequest, CreateNewAppointmentResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor; 

    public CreateNewAppointmentHandler(
        IUnitOfWork unitOfWork,
        IHttpContextAccessor contextAccessor
    )
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

    public async Task<CreateNewAppointmentResponse> ExecuteAsync(
        CreateNewAppointmentRequest command,
        CancellationToken ct
    ) { 
        
        var role = _contextAccessor.HttpContext.User.FindFirstValue(claimType: "role");

        if(!Equals(objA: role, objB: "user")) {
            return new() {
                StatusCode = CreateNewAppointmentResponseStatusCode.FORBIDEN_ACCESS
            };
        } 

        var userId = _contextAccessor.HttpContext.User.FindFirstValue(claimType: "sub");

        if(Equals(objA: userId, objB: null)) {
            return new() {
                StatusCode = CreateNewAppointmentResponseStatusCode.USER_IS_NOT_FOUND
            };
        }

        return new() {
            StatusCode = CreateNewAppointmentResponseStatusCode.OPERATION_SUCCESS
        };
     }
}
