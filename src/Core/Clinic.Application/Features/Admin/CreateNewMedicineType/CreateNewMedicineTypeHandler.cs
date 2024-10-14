using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Commons.Constance;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.Application.Features.Admin.CreateNewMedicineType;

internal sealed class CreateNewMedicineTypeHandler
    : IFeatureHandler<CreateNewMedicineTypeRequest, CreateNewMedicineTypeResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public CreateNewMedicineTypeHandler(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
    {
        _unitOfWork = unitOfWork;
        _contextAccessor = contextAccessor;
    }

    /// <summary>
    /// Empty implementation.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="ct"></param>
    /// <returns></returns> <summary>
    ///
    /// </summary>
    /// <param name="command"></param>
    /// <param name="ct">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///  A task containing the response.
    /// </returns>

    public async Task<CreateNewMedicineTypeResponse> ExecuteAsync(
        CreateNewMedicineTypeRequest command,
        CancellationToken ct
    )
    {
        //Get role
        var role = _contextAccessor.HttpContext.User.FindFirstValue(claimType: "role");

        //Check if role is not admin
        if (Equals(objA: role, objB: "user"))
        {
            return new() { StatusCode = CreateNewMedicineTypeResponseStatusCode.FORBIDEN_ACCESS };
        }

        //Check if medicine already existed
        var isExisted = await _unitOfWork.CreateNewMedicineTypeRepository.IsExistMedicineType(command.Constant, cancellationToken: ct);

        if (isExisted)
        {
            return new()
            {
                StatusCode = CreateNewMedicineTypeResponseStatusCode.MEDICINE_TYPE_ALREADY_EXISTED,
            };
        }

        //Create new medicine
        var newMedicine = new MedicineType()
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            Constant = command.Constant,
        };

        //Create new medicine into database
        var dbResult = await _unitOfWork.CreateNewMedicineTypeRepository.CreateNewMedicineType(
            medicineType: newMedicine,
            cancellationToken: ct
        );

        //Check if operation failed
        if (!dbResult)
        {
            return new()
            {
                StatusCode = CreateNewMedicineTypeResponseStatusCode.DATABASE_OPERATION_FAIL,
            };
        }

        //Return successful code
        return new()
        {
            StatusCode = CreateNewMedicineTypeResponseStatusCode.OPERATION_SUCCESS,
        };
    }
}
