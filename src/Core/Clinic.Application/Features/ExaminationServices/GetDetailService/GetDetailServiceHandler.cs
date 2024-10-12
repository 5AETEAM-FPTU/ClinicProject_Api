using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Threading;
using Clinic.Application.Features.Admin.GetMedicineById;

namespace Clinic.Application.Features.ExaminationServices.GetDetailService;

/// <summary>
///     GetMedicineById Handler
/// </summary>
public class GetDetailServiceHandler
    : IFeatureHandler<GetDetailServiceRequest, GetDetailServiceResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public GetDetailServiceHandler(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
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
    public async Task<GetDetailServiceResponse> ExecuteAsync(
        GetDetailServiceRequest request,
        CancellationToken cancellationToken
    )
    {

        // check service is existed
        var isServiceExisted = await _unitOfWork.GetDetailServiceRepository
            .IsServiceExisted(
                serviceId: request.ServiceId,
                cancellationToken: cancellationToken
            );

        if (!isServiceExisted) {
            return new GetDetailServiceResponse()
            {
                StatusCode = GetDetailServiceResponseStatusCode.SERVICE_NOT_FOUND
            };
        }

        // Get detail service
        var foundService = await _unitOfWork.GetDetailServiceRepository
            .GetDetailServiceByIdQueryAsync(
                Id: request.ServiceId, 
                cancellationToken: cancellationToken
            );

        // Response successfully.
        return new GetDetailServiceResponse()
        {
            StatusCode = GetDetailServiceResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new()
            {
                Service = new()
                {
                   Id = foundService.Id,
                   Code = foundService.Code,
                   Name = foundService.Name,
                   Price = (int)foundService.Price,
                   Description = foundService.Descripiton,
                   Group = foundService.Group
                }
            }
        };
    }
}
