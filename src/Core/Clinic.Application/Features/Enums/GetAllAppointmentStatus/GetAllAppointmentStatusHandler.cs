
using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Features.UnitOfWorks;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace Clinic.Application.Features.Enums.GetAllAppointmentStatus;

/// <summary>
///     GetAllAppointmentStatus Handler
/// </summary>
public class GetAllAppointmentStatusHandler : IFeatureHandler<GetAllAppointmentStatusRequest, GetAllAppointmentStatusResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllAppointmentStatusHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
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
    public async Task<GetAllAppointmentStatusResponse> ExecuteAsync(
        GetAllAppointmentStatusRequest request,
        CancellationToken cancellationToken
    )
    {
        // Find all appointment status query.
        var statuses = await _unitOfWork.GetAllAppointmentStatusRepository.FindAllAppointmentStatusQueryAsync(
            cancellationToken: cancellationToken
        );

        // Response successfully.
        return new GetAllAppointmentStatusResponse()
        {
            StatusCode = GetAllAppointmentStatusResponseStatusCode.OPERATION_SUCCESS,

            ResponseBody = new()
            {
                AppointmentStatuses = statuses.Select(status => new GetAllAppointmentStatusResponse.Body.AppointmentStatus
                {
                    Id = status.Id,
                    StatusName = status.StatusName,
                    Constant = status.Constant,
                })
            }
        };
    }
}
