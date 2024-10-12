using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;

namespace Clinic.Application.Features.Appointments.UpdateAppointmentDepositPayment;

internal class UpdateAppointmentDepositPaymentHandler
    : IFeatureHandler<UpdateAppoinmentDepositPaymenRequest, UpdateAppoinmentDepositPaymentResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public UpdateAppointmentDepositPaymentHandler(
        IUnitOfWork unitOfWork,
        IHttpContextAccessor contextAccessor
    )
    {
        _unitOfWork = unitOfWork;
        _contextAccessor = contextAccessor;
    }

    public async Task<UpdateAppoinmentDepositPaymentResponse> ExecuteAsync(
        UpdateAppoinmentDepositPaymenRequest command,
        CancellationToken ct
    )
    {
        var role = _contextAccessor.HttpContext.User.FindFirstValue(claimType: "role");

        if (!Equals(objA: role, objB: "admin"))
        {
            return new()
            {
                StatusCode = UpdateAppointmentDepositPaymentResponseStatusCode.FORBIDEN_ACCESS,
            };
        }

        var foundAppointment =
            await _unitOfWork.UpdateAppointmentDepositPaymentRepository.GetAppointmentByIdAsync(
                command.AppointmentId,
                ct
            );

        if (Equals(objA: foundAppointment, objB: default))
        {
            return new()
            {
                StatusCode =
                    UpdateAppointmentDepositPaymentResponseStatusCode.APPOINTMENTS_IS_NOT_FOUND,
            };
        }

        var dbResult =
            await _unitOfWork.UpdateAppointmentDepositPaymentRepository.UpdateAppointmentDepositPaymentCommandAsync(
                foundAppointment.Id,
                command.IsDepositPayment,
                ct
            );

        if (!dbResult)
        {
            return new()
            {
                StatusCode =
                    UpdateAppointmentDepositPaymentResponseStatusCode.DATABASE_OPERATION_FAIL,
            };
        }

        return new()
        {
            StatusCode = UpdateAppointmentDepositPaymentResponseStatusCode.OPERATION_SUCCESS,
        };
    }
}
