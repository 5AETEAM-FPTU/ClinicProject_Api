using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;

namespace Clinic.Application.Features.Admin.GetMedicineTypeById;

/// <summary>
///     GetMedicineTypeById Handler
/// </summary>
public class GetMedicineTypeByIdHandler
    : IFeatureHandler<GetMedicineTypeByIdRequest, GetMedicineTypeByIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public GetMedicineTypeByIdHandler(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
    {
        _unitOfWork = unitOfWork;
        _contextAccessor = contextAccessor;
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
    public async Task<GetMedicineTypeByIdResponse> ExecuteAsync(
        GetMedicineTypeByIdRequest request,
        CancellationToken cancellationToken
    )
    {
        // Find medicine type by medicineId
        var foundMedicine =
            await _unitOfWork.GetMedicineTypeByIdRepository.FindMedicineTypeByIdQueryAsync(
                request.MedicineTypeId,
                cancellationToken
            );

        // Responds if medicine type is not found
        if (Equals(objA: foundMedicine, objB: default))
        {
            return new GetMedicineTypeByIdResponse()
            {
                StatusCode = GetMedicineTypeByIdResponseStatusCode.MEDICINE_TYPE_IS_NOT_FOUND,
            };
        }

        // Response successfully.
        return new GetMedicineTypeByIdResponse()
        {
            StatusCode = GetMedicineTypeByIdResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new()
            {
                Type = new GetMedicineTypeByIdResponse.Body.MedicineType()
                {
                    MedicineTypeId = foundMedicine.Id,
                    Constant = foundMedicine.Constant,
                    Name = foundMedicine.Name,
                },
            },
        };
    }
}
