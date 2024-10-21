using System;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;

namespace Clinic.Application.Features.Doctors.GetMedicalReportById;

/// <summary>
///     GetMedicalReportById Handler
/// </summary>
public class GetMedicalReportByIdHandler
    : IFeatureHandler<GetMedicalReportByIdRequest, GetMedicalReportByIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public GetMedicalReportByIdHandler(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
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
    public async Task<GetMedicalReportByIdResponse> ExecuteAsync(
        GetMedicalReportByIdRequest request,
        CancellationToken cancellationToken
    )
    {
        // Found medical report by reportId
        var foundReport =
            await _unitOfWork.GetMedicalReportByIdRepository.GetMedicalReportByIdQueryAsync(
                request.ReportId,
                cancellationToken
            );

        // Responds if reportId is not found
        if (Equals(objA: foundReport, objB: default))
        {
            return new GetMedicalReportByIdResponse()
            {
                StatusCode = GetMedicalReportByIdResponseStatusCode.REPORT_IS_NOT_FOUND,
            };
        }

        // Response successfully.
        return new GetMedicalReportByIdResponse()
        {
            StatusCode = GetMedicalReportByIdResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new()
            {
                AppointmentId = foundReport.AppointmentId,
                PatientInfor = new GetMedicalReportByIdResponse.Body.PatientInformation()
                {
                    PatientId = foundReport.PatientInformation.Id,
                    Address = foundReport.PatientInformation.Address,
                    Avatar = foundReport.Appointment.Patient.User.Avatar,
                    DOB = foundReport.PatientInformation.DOB,
                    FullName = foundReport.PatientInformation.FullName,
                    Gender = foundReport.PatientInformation.Gender,
                    PhoneNumber = foundReport.PatientInformation.PhoneNumber,
                },
                MedicalReport = new GetMedicalReportByIdResponse.Body.ReportDetail()
                {
                    BloodPressure = foundReport.BloodPresser,
                    Date = foundReport.Appointment.Schedule.StartDate,
                    Diagnosis = foundReport.Diagnosis,
                    GeneralCondition = foundReport.GeneralCondition,
                    Height = foundReport.Height,
                    MedicalHistory = foundReport.MedicalHistory,
                    Pulse = foundReport.Pulse,
                    ReportId = foundReport.Id,
                    Temperature = foundReport.Temperature,
                    Weight = foundReport.Weight,
                },
                Medicine = new GetMedicalReportByIdResponse.Body.MedicineOreder()
                {
                    MedicineOrderId = foundReport.MedicineOrder.Id,
                },
                Service = new GetMedicalReportByIdResponse.Body.ServiceOrder()
                {
                    ServiceOrderId = foundReport.ServiceOrder.Id,
                    Quantity = foundReport.ServiceOrder.Quantity,
                    TotalPrice = foundReport.ServiceOrder.TotalPrice,
                },
            },
        };
    }
}
