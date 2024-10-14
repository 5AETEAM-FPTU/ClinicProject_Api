using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;

namespace Clinic.Application.Features.Admin.GetMedicineGroupById;

/// <summary>
///     GetMedicineGroupById Handler
/// </summary>
public class GetMedicineGroupByIdHandler
    : IFeatureHandler<GetMedicineGroupByIdRequest, GetMedicineGroupByIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public GetMedicineGroupByIdHandler(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
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
    public async Task<GetMedicineGroupByIdResponse> ExecuteAsync(
        GetMedicineGroupByIdRequest request,
        CancellationToken cancellationToken
    )
    {
        // Find medicine group by medicineId
        var foundMedicine =
            await _unitOfWork.GetMedicineGroupByIdRepository.FindMedicineGroupByIdQueryAsync(
                request.MedicineGroupId,
                cancellationToken
            );

        // Responds if medicine type is not found
        if (Equals(objA: foundMedicine, objB: default))
        {
            return new GetMedicineGroupByIdResponse()
            {
                StatusCode = GetMedicineGroupByIdResponseStatusCode.MEDICINE_GROUP_IS_NOT_FOUND,
            };
        }

        // Response successfully.
        return new GetMedicineGroupByIdResponse()
        {
            StatusCode = GetMedicineGroupByIdResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new()
            {
                Group = new GetMedicineGroupByIdResponse.Body.MedicineGroup()
                {
                    MedicineGroupId = foundMedicine.Id,
                    Constant = foundMedicine.Constant,
                    Name = foundMedicine.Name,
                },
            },
        };
    }
}
