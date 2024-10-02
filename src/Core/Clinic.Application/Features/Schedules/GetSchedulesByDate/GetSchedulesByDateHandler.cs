using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Commons.FIleObjectStorage;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Clinic.Application.Features.Schedules.GetSchedulesByDate;

/// <summary>
///     GetSchedulesByDate Handler
/// </summary>
public class GetSchedulesByDateHandler
    : IFeatureHandler<GetSchedulesByDateRequest, GetSchedulesByDateResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetSchedulesByDateHandler(IUnitOfWork unitOfWork)
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
    public async Task<GetSchedulesByDateResponse> ExecuteAsync(
        GetSchedulesByDateRequest request,
        CancellationToken cancellationToken
    )
    {
        // Handle date
        var startDate = request.Date.Date;
        var endDate = startDate.AddDays(1).AddTicks(-1);

        // Get Schedules
        var schedules = await _unitOfWork.GetSchedulesByDateRepository.GetSchedulesByDateQueryAsync(
            startDate: startDate,
            endDate: endDate,
            cancellationToken: cancellationToken
        );

        // Response successfully.
        return new GetSchedulesByDateResponse()
        {
            StatusCode = GetSchedulesByDateResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new()
            {
                TimeSlots = schedules
                    .Select(schedule => new GetSchedulesByDateResponse.Body.TimeSlot()
                    {
                        StartTime = schedule.StartDate,
                        EndTime = schedule.EndDate,
                        IsHadAppointment = schedule.Appointment != null
                    })
                    .ToList()
            }
        };
    }
}
