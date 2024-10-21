using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Features.UnitOfWorks;

namespace Clinic.Application.Features.Doctors.GetIdsDoctor;

/// <summary>
///     GetIdsDoctor Handler
/// </summary>
public class GetIdsDoctorHandler : IFeatureHandler<GetIdsDoctorRequest, GetIdsDoctorResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetIdsDoctorHandler(IUnitOfWork unitOfWork)
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
    public async Task<GetIdsDoctorResponse> ExecuteAsync(
        GetIdsDoctorRequest request,
        CancellationToken cancellationToken
    )
    {
        // Get doctor on duty.
        var doctorIds = await _unitOfWork.GetIdsDoctorRepository.FindIdsDoctorQueryAsync(
            cancellationToken
        );

        // Response successfully.
        return new GetIdsDoctorResponse()
        {
            StatusCode = GetIdsDoctorResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new()
            {
                Doctors = doctorIds.Select(id => new GetIdsDoctorResponse.Body.Doctor() { Id = id })
            },
        };
    }
}
