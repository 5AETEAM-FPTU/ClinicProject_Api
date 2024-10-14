using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Threading;
using System.Security.Claims;
using Clinic.Application.Commons.Pagination;
using System.Linq;
using System;

namespace Clinic.Application.Features.Admin.GetAllMedicine;

/// <summary>
///     GetAllMedicine Handler
/// </summary>
public class GetAllMedicineHandler : IFeatureHandler<GetAllMedicineRequest, GetAllMedicineResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public GetAllMedicineHandler(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
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
    public async Task<GetAllMedicineResponse> ExecuteAsync(
        GetAllMedicineRequest request,
        CancellationToken cancellationToken
    )
    {
        // Check role user from role type jwt
        var role = _contextAccessor.HttpContext.User.FindFirstValue(claimType: "role");
        if (!role.Equals("admin"))
        {
            return new()
            {
                StatusCode = GetAllMedicineResponseStatusCode.ROLE_IS_NOT_ADMIN,
            };
        }

        // Find all medicines query.
        var medicines = await _unitOfWork.GetAllMedicineRepository.FindAllMedicinesQueryAsync(
            pageIndex: request.PageIndex,
            pageSize: request.PageSize,
            medicineName: request.Name,
            medicineGroupId: request.MedicineGroupId,
            medicineTypeId: request.MedicineTypeId,
            cancellationToken: cancellationToken
        );

        // Count all the medicines.
        var countMedicine = await _unitOfWork.GetAllMedicineRepository.CountAllMedicinesQueryAsync(
            medicineName: request.Name,
            medicineGroupId: request.MedicineGroupId,
            medicineTypeId: request.MedicineTypeId,
            cancellationToken: cancellationToken
        );

        // Response successfully.
        return new GetAllMedicineResponse()
        {
            StatusCode = GetAllMedicineResponseStatusCode.OPERATION_SUCCESS,

            ResponseBody = new()
            {
                Medicines = new PaginationResponse<GetAllMedicineResponse.Body.Medicine>()
                {
                    Contents = medicines
                    .Select(medicine => new GetAllMedicineResponse.Body.Medicine()
                    {
                        MedicineName = medicine.Name,
                        Ingredient = medicine.Ingredient,
                        MedicineId = medicine.Id,
                        Manufacture = medicine.Manufacture,
                        Group = new GetAllMedicineResponse.Body.Medicine.MedicineGroup()
                        {
                            GroupId = medicine.MedicineGroup.Id,
                            Constant = medicine.MedicineGroup.Constant,
                            Name = medicine.MedicineGroup.Name,
                        },
                        Type = new GetAllMedicineResponse.Body.Medicine.MedicineType()
                        {
                            TypeId = medicine.MedicineType.Id,
                            Name = medicine.MedicineType.Name,
                            Constant = medicine.MedicineType.Constant,
                        }
                    }),
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize,
                    TotalPages = (int)Math.Ceiling((double)countMedicine / request.PageSize)
                }
            }
        };
    }
}
