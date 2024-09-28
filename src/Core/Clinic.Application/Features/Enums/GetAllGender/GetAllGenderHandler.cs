using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Features.UnitOfWorks;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace Clinic.Application.Features.Enums.GetAllGender;

/// <summary>
///     GetAllGender Handler
/// </summary>
public class GetAllGenderHandler : IFeatureHandler<GetAllGenderRequest, GetAllGenderResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllGenderHandler(IUnitOfWork unitOfWork)
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
    public async Task<GetAllGenderResponse> ExecuteAsync(
        GetAllGenderRequest request,
        CancellationToken cancellationToken
    )
    {
        // Find all appointment status query.
        var genders = await _unitOfWork.GetAllGenderRepository.FindAllGenderQueryAsync(
            cancellationToken: cancellationToken
        );

        // Response successfully.
        return new GetAllGenderResponse()
        {
            StatusCode = GetAllGenderResponseStatusCode.OPERATION_SUCCESS,

            ResponseBody = new()
            {
                Genders = genders.Select(gender => new GetAllGenderResponse.Body.Gender
                {
                    Id = gender.Id,
                    GenderName = gender.Name,
                    Constant = gender.Constant,
                })
            }
        };
    }
}
