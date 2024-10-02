using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;

namespace Clinic.Application.Features.Appointments.UpdateAppointmentDepositPayment;

internal class UpdateAppointmentDepositPaymentHandler : IFeatureHandler<UpdateAppoinmentDepositPaymenRequest, UpdateAppoinmentDepositPaymentResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public UpdateAppointmentDepositPaymentHandler(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
    {
        _unitOfWork = unitOfWork;
        _contextAccessor = contextAccessor;
    }

    public async Task<UpdateAppoinmentDepositPaymentResponse> ExecuteAsync(UpdateAppoinmentDepositPaymenRequest command, CancellationToken ct)
    {
        var role = _contextAccessor.HttpContext.User.FindFirstValue(claimType: "role");

        if (!Equals(objA: role, objB: "user"))
        {
            return new() { StatusCode = UpdateAppointmentDepositPaymentResponseStatusCode.FORBIDEN_ACCESS };
        }

        var foundAppointment = null;

        return new() {
            StatusCode = UpdateAppointmentDepositPaymentResponseStatusCode.OPERATION_SUCCESS
        };
    }
}