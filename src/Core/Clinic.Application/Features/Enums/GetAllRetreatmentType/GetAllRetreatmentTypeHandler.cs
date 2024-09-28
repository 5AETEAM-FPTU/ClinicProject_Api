using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Features.UnitOfWorks;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace Clinic.Application.Features.Enums.GetAllRetreatmentType;

/// <summary>
///     GetAllRetreatmentType Handler
/// </summary>
public class GetAllRetreatmentTypeHandler : IFeatureHandler<GetAllRetreatmentTypeRequest, GetAllRetreatmentTypeResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllRetreatmentTypeHandler(IUnitOfWork unitOfWork)
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
    public async Task<GetAllRetreatmentTypeResponse> ExecuteAsync(
        GetAllRetreatmentTypeRequest request,
        CancellationToken cancellationToken
    )
    {
        // Find all appointment status query.
        var types = await _unitOfWork.GetAllRetreatmentTypeRepository.FindAllRetreatmentTypeQueryAsync(
            cancellationToken: cancellationToken
        );

        // Response successfully.
        return new GetAllRetreatmentTypeResponse()
        {
            StatusCode = GetAllRetreatmentTypeResponseStatusCode.OPERATION_SUCCESS,

            ResponseBody = new()
            {
                RetreamentTypes = types.Select(type => new GetAllRetreatmentTypeResponse.Body.RetreamentType
                {
                    Id = type.Id,
                    TypeName = type.Name,
                    Constant = type.Constant,
                })
            }
        };
    }
}
