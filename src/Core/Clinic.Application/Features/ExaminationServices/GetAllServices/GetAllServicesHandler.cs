using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Threading;
using System.Security.Claims;
using Clinic.Application.Commons.Pagination;
using System.Linq;
using System;
using Clinic.Application.Features.Admin.GetAllMedicine;

namespace Clinic.Application.Features.ExaminationServices.GetAllServices;

/// <summary>
///     GetAllServices Handler
/// </summary>
public class GetAllServicesHandler : IFeatureHandler<GetAllServicesRequest, GetAllServicesResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public GetAllServicesHandler(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
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
    public async Task<GetAllServicesResponse> ExecuteAsync(
        GetAllServicesRequest request,
        CancellationToken cancellationToken
    )
    {
        // Check role user from role type jwt
        var role = _contextAccessor.HttpContext.User.FindFirstValue(claimType: "role");
        if (!role.Equals("admin") && !role.Equals("staff"))
        {
            return new()
            {
                StatusCode = GetAllServicesResponseStatusCode.ROLE_IS_NOT_ADMIN_STAFF,
            };
        }

        // Find all medicines query.
        var services = await _unitOfWork.GetAllServicesRepository.FindAllServicesQueryAsync(
            pageIndex: request.PageIndex,
            pageSize: request.PageSize,
            key: request.CodeOrName,
            cancellationToken: cancellationToken
        );

        // Count all the medicines.
        var countService = await _unitOfWork.GetAllServicesRepository.CountAllServicesQueryAsync(
            key: request.CodeOrName,
            cancellationToken: cancellationToken
        );

        // Response successfully.
        return new GetAllServicesResponse()
        {
            StatusCode = GetAllServicesResponseStatusCode.OPERATION_SUCCESS,

            ResponseBody = new()
            {
                Services = new PaginationResponse<GetAllServicesResponse.Body.Service>()
                {
                    Contents = services
                    .Select(service => new GetAllServicesResponse.Body.Service()
                    {
                        Id = service.Id,
                        Name = service.Name,
                        Code = service.Code,
                        Price = (int) service.Price,
                        Group = service.Group,
                        Description = service.Descripiton
                    }),
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize,
                    TotalPages = (int)Math.Ceiling((double)countService / request.PageSize)
                }
            }
        };
    }
}
