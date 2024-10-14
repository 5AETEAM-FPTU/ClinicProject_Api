using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Features.UnitOfWorks;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace Clinic.Application.Features.Admin.GetAllMedicineGroup;

/// <summary>
///     GetAllMedicineGroup Handler
/// </summary>
public class GetAllMedicineGroupHandler : IFeatureHandler<GetAllMedicineGroupRequest, GetAllMedicineGroupResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllMedicineGroupHandler(IUnitOfWork unitOfWork)
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
    public async Task<GetAllMedicineGroupResponse> ExecuteAsync(
        GetAllMedicineGroupRequest request,
        CancellationToken cancellationToken
    )
    {
        // Find all medicine group query.
        var groups = await _unitOfWork.GetAllMedicineGroupRepository.FindAllMedicineGroupQueryAsync(
            cancellationToken: cancellationToken
        );

        // Response successfully.
        return new GetAllMedicineGroupResponse()
        {
            StatusCode = GetAllMedicineGroupResponseStatusCode.OPERATION_SUCCESS,

            ResponseBody = new()
            {
                Group = groups.Select(group => new GetAllMedicineGroupResponse.Body.MedicineGroup()
                {
                    Constant = group.Constant,
                    MedicineGroupId = group.Id,
                    Name = group.Name,
                })
            }
        };
    }
}
