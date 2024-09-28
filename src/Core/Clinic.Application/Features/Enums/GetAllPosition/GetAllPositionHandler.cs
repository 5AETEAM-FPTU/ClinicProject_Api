using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Features.UnitOfWorks;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace Clinic.Application.Features.Enums.GetAllPosition;

/// <summary>
///     GetAllPosition Handler
/// </summary>
public class GetAllPositionHandler : IFeatureHandler<GetAllPositionRequest, GetAllPositionResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllPositionHandler(IUnitOfWork unitOfWork)
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
    public async Task<GetAllPositionResponse> ExecuteAsync(
        GetAllPositionRequest request,
        CancellationToken cancellationToken
    )
    {
        // Find all appointment status query.
        var positions = await _unitOfWork.GetAllPositionRepository.FindAllPositionQueryAsync(
            cancellationToken: cancellationToken
        );

        // Response successfully.
        return new GetAllPositionResponse()
        {
            StatusCode = GetAllPositionResponseStatusCode.OPERATION_SUCCESS,

            ResponseBody = new()
            {
                Positions = positions.Select(position => new GetAllPositionResponse.Body.Position
                {
                    Id = position.Id,
                    PositionName = position.Name,
                    Constant = position.Constant,
                })
            }
        };
    }
}