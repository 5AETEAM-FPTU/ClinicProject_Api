using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;
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

        var startOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1); // Ngày đầu tiên của tháng hiện tại
        var lastDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)).AddDays(1).AddTicks(-1);
        var startOfYear = new DateTime(DateTime.Now.Year, 1, 1);
        var endOfYear = new DateTime(DateTime.Now.Year + 1, 1, 1).AddTicks(-1); 
        

        var revenueInCurrentMonth = await _unitOfWork.GetStaticInformationRepository.getTotalRevenueByDate(startOfMonth, lastDayOfMonth, ct);
        var revenueInCurrentYear = await _unitOfWork.GetStaticInformationRepository.getTotalRevenueByDate(startOfYear, endOfYear, ct);
        var newUserInCurrentMonth = await _unitOfWork.GetStaticInformationRepository.getNewUserInSystemByDate(startOfMonth, lastDayOfMonth, ct);
        var totalDoctor = await _unitOfWork.GetStaticInformationRepository.getTotalUserByRole("doctor", ct);
        var totalStaff = await _unitOfWork.GetStaticInformationRepository.getTotalUserByRole("staff", ct);
        var totalPatient = await _unitOfWork.GetStaticInformationRepository.getTotalUserByRole("user", ct);
        var averageFeedback = await _unitOfWork.GetStaticInformationRepository.GetAverageRatingFeedback(ct);
        var appointmentInCurrentMonth = await _unitOfWork.GetStaticInformationRepository.countAppointmentByDate(startOfMonth, lastDayOfMonth, ct);
        var monthlyRevenue = await _unitOfWork.GetStaticInformationRepository.getMonthlyRevenue(ct);
        var monthlyAppointment = await _unitOfWork.GetStaticInformationRepository.getMonthLyAppointment(ct);
        return new GetStaticInformationResponse()
        {
            StatusCode = GetStaticInformationResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new
            {
                monthlyRevenue,
                revenueInCurrentMonth,
                revenueInCurrentYear,
                totalDoctor,
                totalStaff,
                totalPatient,
                newUserInCurrentMonth,
                averageFeedback,
                appointmentInCurrentMonth,
                monthlyAppointment
            }
        };
    }


}
