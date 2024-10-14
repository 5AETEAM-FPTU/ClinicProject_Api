using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Threading;

namespace Clinic.Application.Features.Admin.GetMedicineById;

/// <summary>
///     GetMedicineById Handler
/// </summary>
public class GetMedicineByIdHandler
    : IFeatureHandler<GetMedicineByIdRequest, GetMedicineByIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public GetMedicineByIdHandler(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
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
    public async Task<GetMedicineByIdResponse> ExecuteAsync(
        GetMedicineByIdRequest request,
        CancellationToken cancellationToken
    )
    {
        // Find medicine by medicineId
        var foundMedicine =
            await _unitOfWork.GetMedicineByIdRepository.FindMedicineByIdQueryAsync(
                    request.MedicineId,
                    cancellationToken
                );

        // Responds if medicine is not found
        if (Equals(objA: foundMedicine, objB: default))
        {
            return new GetMedicineByIdResponse()
            {
                StatusCode = GetMedicineByIdResponseStatusCode.MEDICINE_IS_NOT_FOUND
            };
        }

        // Response successfully.
        return new GetMedicineByIdResponse()
        {
            StatusCode = GetMedicineByIdResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new()
            {
                Medicine = new GetMedicineByIdResponse.Body.MedicineDetail()
                {
                    MedicineId = foundMedicine.Id,
                    Ingredient = foundMedicine.Ingredient,
                    Manufacture = foundMedicine.Manufacture,
                    MedicineName = foundMedicine.Name,
                    Group = new GetMedicineByIdResponse.Body.MedicineDetail.MedicineGroup()
                    {
                        GroupId = foundMedicine.MedicineGroup.Id,
                        Constant = foundMedicine.MedicineGroup.Constant,
                        Name = foundMedicine.MedicineGroup.Name,
                    },
                    Type = new GetMedicineByIdResponse.Body.MedicineDetail.MedicineType()
                    {
                        TypeId = foundMedicine.MedicineType.Id,
                        Constant = foundMedicine.MedicineType.Constant,
                        Name = foundMedicine.MedicineType.Name,
                    },
                }
            }
        };
    }
}
