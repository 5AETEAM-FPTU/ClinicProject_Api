using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Appointments.UpdateAppointmentStatus;
internal class UpdateAppointmentStatusHandler : IFeatureHandler<UpdateAppointmentStatusRequest, UpdateAppointmentStatusResponse>
{
    public async Task<UpdateAppointmentStatusResponse> ExecuteAsync(UpdateAppointmentStatusRequest command, CancellationToken ct)
    {
        return new() {
            StatusCode = UpdateAppointmentStatusResponseStatusCode.OPERATION_SUCCESS
        };
    }
}