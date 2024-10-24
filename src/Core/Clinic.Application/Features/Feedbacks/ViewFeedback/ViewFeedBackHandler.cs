using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;

namespace Clinic.Application.Features.Feedbacks.ViewFeedback;

internal sealed class ViewFeedBackHandler
    : IFeatureHandler<ViewFeedBackRequest, ViewFeedBackResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public ViewFeedBackHandler(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
    {
        _unitOfWork = unitOfWork;
        _contextAccessor = contextAccessor;
    }

    /// <summary>
    /// Empty implementation.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    /// <returns></returns> <summary>
    ///
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ct">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///  A task containing the response.
    /// </returns>

    public async Task<ViewFeedBackResponse> ExecuteAsync(
        ViewFeedBackRequest request,
        CancellationToken ct
    )
    {
        // Get role
        var role = _contextAccessor.HttpContext.User.FindFirstValue(claimType: "role");

        //Check if role is not user
        if (!Equals(objA: role, objB: "user"))
        {
            return new() { StatusCode = ViewFeedBackResponseStatusCode.FORBIDDEN };
        }

        // foundAppointment
        var foundAppointment = await _unitOfWork.ViewFeedbackRepository.GetAppointmentByIdQueryAsync(
             request.AppointmentId,
             cancellationToken: ct
        );

        if(foundAppointment == null)
        {
            return new() { StatusCode = ViewFeedBackResponseStatusCode.APPOINTMENT_NOT_FOUND };
        }

        // Check if feedback already existed
        var isFeedbackExisted = await _unitOfWork.ViewFeedbackRepository.IsExistFeedback(
            foundAppointment.Id,
            cancellationToken: ct
        );

        if (!isFeedbackExisted)
        {
            return new() { StatusCode = ViewFeedBackResponseStatusCode.FEEDBACK_HAVE_NOT_SENT };
        }

        // Get doctor releated appointment
        var doctorResult = await _unitOfWork.ViewFeedbackRepository.GetDoctorByIdQueryAsync(
            appointmentId: foundAppointment.Id,
            cancellationToken: ct
        );

        // Get doctor's rating
        var ratingResult = await _unitOfWork.ViewFeedbackRepository.GetRatingQueryAsync(
            doctorId: foundAppointment.Schedule.DoctorId,
            cancellationToken: ct
        );

        // Get feedback
        var feedbackResult = await _unitOfWork.ViewFeedbackRepository.GetFeedBackQueryAsync(
            appointmentId: foundAppointment.Id,
            cancellationToken: ct
        );



        //Return successful code
        return new ViewFeedBackResponse()
        {
            StatusCode = ViewFeedBackResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new ViewFeedBackResponse.Body()
            {
                Feedback = new ViewFeedBackResponse.Body.FeedbackDetail()
                {
                    Id = feedbackResult.Id,
                    Vote = feedbackResult.Vote,
                    Comment = feedbackResult.Comment,   
                },
                Doctor = new ViewFeedBackResponse.Body.DoctorInfo()
                {
                    Fullname = doctorResult.FullName,
                    AvatarUrl = doctorResult.Avatar,
                    Rating = Math.Round(ratingResult,1),
                    Specialties = doctorResult.Doctor.DoctorSpecialties.Select(item => new ViewFeedBackResponse.Body.DoctorInfo.Specialty()
                    {
                        Id = item.Specialty.Id,
                        Name = item.Specialty.Name, 
                        Constant = item.Specialty.Constant, 
                    }),
                    
                }
            }   
        };
    }
}
