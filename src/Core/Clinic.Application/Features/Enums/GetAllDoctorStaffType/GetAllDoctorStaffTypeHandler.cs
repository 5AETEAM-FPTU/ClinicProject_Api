using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.Application.Features.Enums.GetAllDoctorStaffType;

/// <summary>
///     GetAllDoctorStaffType Handler
/// </summary>
public class GetAllDoctorStaffTypeHandler : IFeatureHandler<GetAllDoctorStaffTypeRequest, GetAllDoctorStaffTypeResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public GetAllDoctorStaffTypeHandler(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
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
    public async Task<GetAllDoctorStaffTypeResponse> ExecuteAsync(
        GetAllDoctorStaffTypeRequest request,
        CancellationToken cancellationToken
    )
    {
        var doctorStaffTypes = await _unitOfWork.GetAllDoctorStaffTypeRepository.FindAllDoctorStaffTypesQueryAsync(
           cancellationToken: cancellationToken
       );

        // Response successfully.
        return new GetAllDoctorStaffTypeResponse()
        {
            StatusCode = GetAllDoctorStaffTypeResponseStatusCode.OPERATION_SUCCESS,

            ResponseBody = new()
            {
                DoctorStaffTypes = doctorStaffTypes.Select(doctorStaffType => new GetAllDoctorStaffTypeResponse.Body.DoctorStaffType
                {
                    Id = doctorStaffType.Id,
                    TypeName = doctorStaffType.TypeName,
                    Constant = doctorStaffType.Constant,
                })
            }
        };
    }
}
