using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Commons.Constance;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;

namespace Clinic.Application.Features.OnlinePayments.CreateNewOnlinePayment;

internal sealed class CreateNewOnlinePaymentHandler
    : IFeatureHandler<CreateNewOnlinePaymentRequest, CreateNewOnlinePaymentResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreateNewOnlinePaymentHandler(
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor
    )
    {
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<CreateNewOnlinePaymentResponse> ExecuteAsync(
        CreateNewOnlinePaymentRequest command,
        CancellationToken ct
    )
    {
        var role = _httpContextAccessor.HttpContext.User.FindFirstValue(claimType: "role");
        if (!Equals(objA: role, objB: "user"))
        {
            return new() { StatusCode = CreateNewOnlinePaymentResponseStatusCode.FORBIDEN_ACCESS };
        }

        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(claimType: "sub");

        if (Equals(objA: userId, objB: null))
        {
            return new()
            {
                StatusCode = CreateNewOnlinePaymentResponseStatusCode.USER_IS_NOT_FOUND,
            };
        }

        var isUserHasValidAppointment =
            await _unitOfWork.CreateNewOnlinePaymentRepository.IsUserHasCorrectlyAppointmentWithIdAsync(
                Guid.Parse(userId),
                command.AppointmentId,
                ct
            );

        if (!isUserHasValidAppointment)
        {
            return new()
            {
                StatusCode = CreateNewOnlinePaymentResponseStatusCode.APPOINTMENT_IS_NOT_FOUND,
            };
        }

        var newOnlinePaymentInfo = new OnlinePayment()
        {
            Id = Guid.NewGuid(),
            AppointmentId = command.AppointmentId,
            Amount = (int)command.Amount,
            TransactionID = command.TransactionId,
            PaymentMethod = command.PaymentMethod,
            CreatedBy = Guid.Parse(userId),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = CommonConstant.MIN_DATE_TIME,
            UpdatedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
        };

        var dbResult =
            await _unitOfWork.CreateNewOnlinePaymentRepository.CreateNewOnlinePaymentAsync(
                onlinePayment: newOnlinePaymentInfo,
                cancellationToken: ct
            );

        if (!dbResult)
        {
            return new()
            {
                StatusCode = CreateNewOnlinePaymentResponseStatusCode.DATABASE_OPERATION_FAIL,
            };
        }

        return new() { StatusCode = CreateNewOnlinePaymentResponseStatusCode.OPERATION_SUCCESS };
    }
}
