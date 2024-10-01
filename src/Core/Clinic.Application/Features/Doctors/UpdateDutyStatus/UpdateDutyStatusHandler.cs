using Clinic.Application.Commons.Abstractions;
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

namespace Clinic.Application.Features.Doctors.UpdateDutyStatus;

public class UpdateDutyStatusHandler : IFeatureHandler<UpdateDutyStatusRequest, UpdateDutyStatusResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;
    public UpdateDutyStatusHandler(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor) { 
        _unitOfWork = unitOfWork;
        _contextAccessor = contextAccessor;
    }
    public async Task<UpdateDutyStatusResponse> ExecuteAsync(UpdateDutyStatusRequest req, CancellationToken ct)
    {
        var userId = Guid.Parse(
            _contextAccessor.HttpContext.User.FindFirstValue(claimType: JwtRegisteredClaimNames.Sub)
        );
        
        bool ok = await _unitOfWork.UpdateDutyStatusRepository.UpdateDutyStatusCommandAsync(userId: userId, status: req.Status, cancellationToken: ct);
        var StatusCode = UpdateDutyStatusResponseStatusCode.OPERATION_SUCCESS;
        if (!ok)
        {
            StatusCode = UpdateDutyStatusResponseStatusCode.USER_IS_NOT_FOUND;
        }
        return new UpdateDutyStatusResponse()
        {
            StatusCode = StatusCode
        };

    }
}
