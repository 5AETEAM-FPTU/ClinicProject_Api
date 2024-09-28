
using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Features.UnitOfWorks;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace Clinic.Application.Features.Enums.GetAllSpecialty;

/// <summary>
///     GetAllSpecialty Handler
/// </summary>
public class GetAllSpecialtyHandler : IFeatureHandler<GetAllSpecialtyRequest, GetAllSpecialtyResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllSpecialtyHandler(IUnitOfWork unitOfWork)
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
    public async Task<GetAllSpecialtyResponse> ExecuteAsync(
        GetAllSpecialtyRequest request,
        CancellationToken cancellationToken
    )
    {
        // Find all appointment status query.
        var specialties = await _unitOfWork.GetAllSpecialtyRepository.FindAllSpecialtyQueryAsync(
            cancellationToken: cancellationToken
        );

        // Response successfully.
        return new GetAllSpecialtyResponse()
        {
            StatusCode = GetAllSpecialtyResponseStatusCode.OPERATION_SUCCESS,

            ResponseBody = new()
            {
                Specialties = specialties.Select(specialty => new GetAllSpecialtyResponse.Body.Specialty
                {
                    Id = specialty.Id,
                    SpecialtyName = specialty.Name,
                    Constant = specialty.Constant,
                })
            }
        };
    }
}