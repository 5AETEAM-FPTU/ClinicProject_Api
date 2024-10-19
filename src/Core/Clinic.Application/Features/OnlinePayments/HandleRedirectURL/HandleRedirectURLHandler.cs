using System;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Commons.Constance;
using Clinic.Application.Commons.Payment;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;

namespace Clinic.Application.Features.OnlinePayments.HandleRedirectURL;

/// <summary>
///     HandleRedirectURL Handler
/// </summary>
public class HandleRedirectURLHandler
    : IFeatureHandler<HandleRedirectURLRequest, HandleRedirectURLResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPaymentHandler _paymentHandler;

    public HandleRedirectURLHandler(IUnitOfWork unitOfWork, IPaymentHandler paymentHandler)
    {
        _unitOfWork = unitOfWork;
        _paymentHandler = paymentHandler;
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
    public async Task<HandleRedirectURLResponse> ExecuteAsync(
        HandleRedirectURLRequest request,
        CancellationToken cancellationToken
    )
    {
        // Verify hash key.
        var isFoundKey = _paymentHandler.VerifySecureKey(secureHash: request.HashKey);

        // Respond if hash key is verified fail.
        if (!isFoundKey)
        {
            return new() { StatusCode = HandleRedirectURLResponseStatusCode.FORBIDDEN_ACCESS };
        }

        // Find online payment by appiointment id.
        var existPayment =
            await _unitOfWork.HandleRedirectURLRepository.FindPaymentByAppointmentIdQueryAsync(
                appointmentId: request.AppointmentId,
                cancellationToken: cancellationToken
            );

        // Respond if online payment not found.
        if (Equals(objA: existPayment, objB: default))
        {
            return new HandleRedirectURLResponse()
            {
                StatusCode = HandleRedirectURLResponseStatusCode.PAYMENT_IS_NOT_FOUND,
            };
        }

        // Respond if payment is aleary paid.
        if (Equals(objA: existPayment.UpdatedBy, objB: CommonConstant.SYSTEM_GUID))
        {
            return new()
            {
                StatusCode = HandleRedirectURLResponseStatusCode.PAYMENT_IS_ALREADY_PAID
            };
        }

        if (
            Equals(objA: request.BankTransactionNo, objB: default)
            || Equals(objA: request.TransactionNo, objB: default)
        )
        {
            var isRemovedAppointment =
                await _unitOfWork.HandleRedirectURLRepository.DeleteAppointmentCommandAsync(
                    appointmentId: request.AppointmentId,
                    cancellationToken: cancellationToken
                );

            if (!isRemovedAppointment)
            {
                return new()
                {
                    StatusCode = HandleRedirectURLResponseStatusCode.DATABASE_OPERATION_FAIL
                };
            }
            return new() { StatusCode = HandleRedirectURLResponseStatusCode.RETURN_CANCEL_PAYMENT };
        }

        var newpayment = InitUpdatedPayment(existPayment: existPayment, request: request);

        // Update online payment status.
        var isUpdated = await _unitOfWork.HandleRedirectURLRepository.UpdatePaymentCommandAsync(
            existPayment: existPayment,
            onlinePayment: newpayment,
            cancellationToken: cancellationToken
        );

        // Respond if database operation fail.
        if (!isUpdated)
        {
            return new()
            {
                StatusCode = HandleRedirectURLResponseStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        var appointment =
            await _unitOfWork.HandleRedirectURLRepository.FindAppointmentByIdQueryAsync(
                appointmentId: request.AppointmentId,
                cancellationToken: cancellationToken
            );

        return new HandleRedirectURLResponse()
        {
            StatusCode = HandleRedirectURLResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new HandleRedirectURLResponse.Body()
            {
                Amount = (int.Parse(request.Amount) / 100m).ToString(),
                AppointmentDate = appointment.ExaminationDate,
                PaymentDate = request.PayDate,
                DoctorName = appointment.Schedule.Doctor.User.FullName,
                TransactionId = request.TransactionNo
            }
        };
    }

    private static OnlinePayment InitUpdatedPayment(
        OnlinePayment existPayment,
        HandleRedirectURLRequest request
    )
    {
        return new()
        {
            Id = existPayment.Id,
            AppointmentId = existPayment.AppointmentId,
            CreatedBy = existPayment.CreatedBy,
            CreatedAt = existPayment.CreatedAt,
            Amount = int.Parse(request.Amount),
            UpdatedAt = DateTime.UtcNow,
            UpdatedBy = CommonConstant.SYSTEM_GUID,
            PaymentMethod = request.CardType,
            TransactionID = request.TransactionNo
        };
    }
}
