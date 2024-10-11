using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Threading;

namespace Clinic.Application.Features.Admin.GetMedicineById;

/// <summary>
///     GetMedicineById Handler
/// </summary>
public class GetMedicineByIdHandler
    : IFeatureHandler<GetMedicineByIdRequest, GetMedicineByIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public GetMedicineByIdHandler(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
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
    public async Task<GetMedicineByIdResponse> ExecuteAsync(
        GetMedicineByIdRequest request,
        CancellationToken cancellationToken
    )
    {
        //// Found medical report by reportId
        //var foundReport =
        //    await _unitOfWork.GetMedicalReportByIdRepository.GetMedicalReportByIdQueryAsync(
        //            request.ReportId,
        //            cancellationToken
        //        );

        //// Responds if reportId is not found
        //if (Equals(objA: foundReport, objB: default))
        //{
        //    return new GetMedicalReportByIdResponse()
        //    {
        //        StatusCode = GetMedicalReportByIdResponseStatusCode.REPORT_IS_NOT_FOUND
        //    };
        //}

        // Response successfully.
        return new GetMedicineByIdResponse()
        {
            StatusCode = GetMedicineByIdResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new()
            {

            }
        };
    }
}
