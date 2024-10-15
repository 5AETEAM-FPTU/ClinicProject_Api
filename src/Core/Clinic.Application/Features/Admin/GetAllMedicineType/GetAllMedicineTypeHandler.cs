using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Features.UnitOfWorks;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace Clinic.Application.Features.Admin.GetAllMedicineType;

/// <summary>
///     GetAllMedicineType Handler
/// </summary>
public class GetAllMedicineTypeHandler : IFeatureHandler<GetAllMedicineTypeRequest, GetAllMedicineTypeResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllMedicineTypeHandler(IUnitOfWork unitOfWork)
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
    public async Task<GetAllMedicineTypeResponse> ExecuteAsync(
        GetAllMedicineTypeRequest request,
        CancellationToken cancellationToken
    )
    {
        // Find all medicine type query.
        var types = await _unitOfWork.GetAllMedicineTypeRepository.FindAllMedicineTypeQueryAsync(
            cancellationToken: cancellationToken
        );

        // Response successfully.
        return new GetAllMedicineTypeResponse()
        {
            StatusCode = GetAllMedicineTypeResponseStatusCode.OPERATION_SUCCESS,

            ResponseBody = new()
            {
               Type = types.Select(type => new GetAllMedicineTypeResponse.Body.MedicineType()
               {
                   Constant = type.Constant,
                   MedicineTypeId = type.Id,
                   Name = type.Name,
               })
            }
        };
    }
}
