using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;

namespace Clinic.Application.Features.Doctors.GetAvailableDoctor;

/// <summary>
///     GetAvailableDoctor Handler
/// </summary>
public class GetAvailableDoctorHandler
    : IFeatureHandler<GetAvailableDoctorRequest, GetAvailableDoctorResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAvailableDoctorHandler(IUnitOfWork unitOfWork)
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
    public async Task<GetAvailableDoctorResponse> ExecuteAsync(
        GetAvailableDoctorRequest request,
        CancellationToken cancellationToken
    )
    {
        // Get doctor on duty.
        var doctors = await _unitOfWork.GetAvailableDoctorRepository.GetAvailableDoctorQueryAsync(
            cancellationToken
        );

        // Response successfully.
        return new GetAvailableDoctorResponse()
        {
            StatusCode = GetAvailableDoctorResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new()
            {
                Doctors = doctors.Select(doctor => new GetAvailableDoctorResponse.Body.Doctor()
                {
                    DoctorId = doctor.Id,
                    FullName = doctor.FullName,
                    AvatarUrl = doctor.Avatar,
                })
            },
        };
    }
}
