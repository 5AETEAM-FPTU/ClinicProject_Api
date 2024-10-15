using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Security.Claims;
using Clinic.Domain.Commons.Entities;

namespace Clinic.Application.Features.Admin.CreateNewMedicineGroup;

internal sealed class CreateNewMedicineGroupHandler
    : IFeatureHandler<CreateNewMedicineGroupRequest, CreateNewMedicineGroupResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public CreateNewMedicineGroupHandler(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
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

    public async Task<CreateNewMedicineGroupResponse> ExecuteAsync(
        CreateNewMedicineGroupRequest command,
        CancellationToken ct
    )
    {
        //Get role
        var role = _contextAccessor.HttpContext.User.FindFirstValue(claimType: "role");

        //Check if role is not admin
        if (Equals(objA: role, objB: "user"))
        {
            return new() { StatusCode = CreateNewMedicineGroupResponseStatusCode.FORBIDEN_ACCESS };
        }

        //Check if medicine already existed
        var isExisted = await _unitOfWork.CreateNewMedicineGroupRepository.IsExistMedicineGroup(command.Constant, cancellationToken: ct);

        if (isExisted)
        {
            return new()
            {
                StatusCode = CreateNewMedicineGroupResponseStatusCode.MEDICINE_GROUP_ALREADY_EXISTED,
            };
        }

        //Create new medicine
        var newMedicine = new MedicineGroup()
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            Constant = command.Constant,
        };

        //Create new medicine into database
        var dbResult = await _unitOfWork.CreateNewMedicineGroupRepository.CreateNewMedicineGroup(
            medicineGroup: newMedicine,
            cancellationToken: ct
        );

        //Check if operation failed
        if (!dbResult)
        {
            return new()
            {
                StatusCode = CreateNewMedicineGroupResponseStatusCode.DATABASE_OPERATION_FAIL,
            };
        }

        //Return successful code
        return new()
        {
            StatusCode = CreateNewMedicineGroupResponseStatusCode.OPERATION_SUCCESS,
        };
    }
}
