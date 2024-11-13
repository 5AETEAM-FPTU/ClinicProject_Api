using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.Application.Features.Admin.GetStaticInformation;

/// <summary>
///     GetStaticInformation Handler
/// </summary>
public class GetStaticInformationHandler : IFeatureHandler<GetStaticInformationRequest, GetStaticInformationResponse>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public GetStaticInformationHandler(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
    {
        _unitOfWork = unitOfWork;
        _contextAccessor = contextAccessor;
    }

    public async Task<GetStaticInformationResponse> ExecuteAsync(GetStaticInformationRequest command, CancellationToken ct)
    {
        var role = _contextAccessor.HttpContext.User.FindFirstValue(claimType: "role");
        if (!role.Equals("admin"))
        {
            return new GetStaticInformationResponse()
            {
                StatusCode = GetStaticInformationResponseStatusCode.USER_IS_NOT_ADMIN,
            };
        }

        var response = await _unitOfWork.GetStaticInformationRepository.getFastOverviewInformation(command.year, ct);
        return new GetStaticInformationResponse()
        {
            StatusCode = GetStaticInformationResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = response
        };
    }


}
