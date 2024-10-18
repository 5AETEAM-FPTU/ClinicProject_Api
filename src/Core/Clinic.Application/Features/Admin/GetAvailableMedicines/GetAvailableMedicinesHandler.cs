using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;

namespace Clinic.Application.Features.Admin.GetAvailableMedicines;

/// <summary>
///     GetAvailableMedicines Handler
/// </summary>
public class GetAvailableMedicinesHandler
    : IFeatureHandler<GetAvailableMedicinesRequest, GetAvailableMedicinesResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public GetAvailableMedicinesHandler(
        IUnitOfWork unitOfWork,
        IHttpContextAccessor contextAccessor
    )
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
    public async Task<GetAvailableMedicinesResponse> ExecuteAsync(
        GetAvailableMedicinesRequest request,
        CancellationToken cancellationToken
    )
    {
        // Find all medicines query.
        var medicines =
            await _unitOfWork.GetAvailableMedicinesRepository.FindAvailableMedicinesQueryAsync(
                medicineName: request.Name,
                medicineGroupId: request.MedicineGroupId,
                medicineTypeId: request.MedicineTypeId,
                cancellationToken: cancellationToken
            );

        // Response successfully.
        return new GetAvailableMedicinesResponse()
        {
            StatusCode = GetAvailableMedicinesResponseStatusCode.OPERATION_SUCCESS,

            ResponseBody = new()
            {
                Medicines = medicines.Select(
                    medicine => new GetAvailableMedicinesResponse.Body.Medicine()
                    {
                        MedicineName = medicine.Name,
                        Ingredient = medicine.Ingredient,
                        MedicineId = medicine.Id,
                        Manufacture = medicine.Manufacture,
                        Group = new GetAvailableMedicinesResponse.Body.Medicine.MedicineGroup()
                        {
                            GroupId = medicine.MedicineGroup.Id,
                            Constant = medicine.MedicineGroup.Constant,
                            Name = medicine.MedicineGroup.Name,
                        },
                        Type = new GetAvailableMedicinesResponse.Body.Medicine.MedicineType()
                        {
                            TypeId = medicine.MedicineType.Id,
                            Name = medicine.MedicineType.Name,
                            Constant = medicine.MedicineType.Constant,
                        },
                    }
                ),
            },
        };
    }
}
