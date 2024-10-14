using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Threading;
using System.Security.Claims;
using System.Linq;
using System;

namespace Clinic.Application.Features.ExaminationServices.GetAvailableServices;

/// <summary>
///     GetAvailableServices Handler
/// </summary>
public class GetAvailableServicesHandler : IFeatureHandler<GetAvailableServicesRequest, GetAvailableServicesResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public GetAvailableServicesHandler(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
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
    public async Task<GetAvailableServicesResponse> ExecuteAsync(
        GetAvailableServicesRequest request,
        CancellationToken cancellationToken
    )
    {
        // Check role user from role type jwt
        var role = _contextAccessor.HttpContext.User.FindFirstValue(claimType: "role");
        if (!role.Equals("admin") && !role.Equals("staff"))
        {
            return new()
            {
                StatusCode = GetAvailableServicesResponseStatusCode.ROLE_IS_NOT_ADMIN_STAFF,
            };
        }

        // Find all available services query.
        var foundServices = await _unitOfWork.GetAvailableServicesRepository.GetAvailableServicesQueryAsync(
            key: request.CodeOrName,
            cancellationToken: cancellationToken
        );

        // Response successfully.
        return new GetAvailableServicesResponse()
        {
            StatusCode = GetAvailableServicesResponseStatusCode.OPERATION_SUCCESS,

            ResponseBody = new()
            {
                Services = foundServices.Select(service => new GetAvailableServicesResponse.Body.Service () 
                { 
                    Id = service.Id, 
                    Name = service.Name, 
                    Code = service.Code,
                    Description = service.Descripiton, 
                    Price = (int)service.Price, 
                    Group = service.Group 
                })
            }
        };
    }
}
