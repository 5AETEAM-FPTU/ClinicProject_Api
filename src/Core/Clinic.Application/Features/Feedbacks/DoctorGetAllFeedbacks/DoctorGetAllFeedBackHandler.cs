using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Commons.Pagination;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Clinic.Application.Features.Feedbacks.DoctorGetAllFeedbacks;

internal sealed class DoctorGetAllFeedBackHandler
    : IFeatureHandler<DoctorGetAllFeedBackRequest, DoctorGetAllFeedBackResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly UserManager<User> _userManager;

    public DoctorGetAllFeedBackHandler(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor, UserManager<User> userManager)
    {
        _unitOfWork = unitOfWork;
        _contextAccessor = contextAccessor;
        _userManager = userManager;
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

    public async Task<DoctorGetAllFeedBackResponse> ExecuteAsync(
        DoctorGetAllFeedBackRequest request,
        CancellationToken ct
    )
    {
        // Check if role is not user
        var role = _contextAccessor.HttpContext.User.FindFirstValue(claimType: "role");
        
        if (!Equals(objA: role, objB: "doctor"))
        {
            return new() { StatusCode = DoctorGetAllFeedBackResponseStatusCode.FORBIDDEN };
        }

        // get userId from jwt
        var userId = Guid.Parse(
            _contextAccessor.HttpContext.User.FindFirstValue(claimType: JwtRegisteredClaimNames.Sub)
        );


        
        // get all feedback
        var feedbacksResult = await _unitOfWork.DoctorGetAllFeedbacksRepository.GetAllFeedbacksQueryAsync(
                pageIndex: request.PageIndex,
                pageSize: request.PageSize,
                userId: userId,
                vote: request.Vote,
                cancellationToken: ct
            );

        // for get related patient of feedback
        var feedbackResponse = new List<DoctorGetAllFeedBackResponse.Body.Feedback>();

        foreach (var feedback in feedbacksResult)
        {
            var user = await _userManager.FindByIdAsync(feedback.CreatedBy.ToString());

            feedbackResponse.Add(new DoctorGetAllFeedBackResponse.Body.Feedback()
            {
                Id = feedback.Id,
                Comment = feedback.Comment,
                Vote = feedback.Vote,
                CreatedAt = feedback.CreatedAt,
                PatientName = user?.FullName,
                AvatarUrl = user?.Avatar
            });
        }

        // count feedback
        var countFeedback = await _unitOfWork.DoctorGetAllFeedbacksRepository.CountAllFeedbacksQueryAsync(
            userId: userId,
            vote: request.Vote,
            cancellationToken: ct
        );

        // get rating of doctor
        var rating = await _unitOfWork.DoctorGetAllFeedbacksRepository.GetRatingQueryAsync(
                userId: userId,
                cancellationToken: ct
            );

        //Return successful code
        return new DoctorGetAllFeedBackResponse()
        {
            StatusCode = DoctorGetAllFeedBackResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new DoctorGetAllFeedBackResponse.Body()
            {
                Feedbacks = new PaginationResponse<DoctorGetAllFeedBackResponse.Body.Feedback>()
                {
                    Contents = feedbackResponse,
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize,
                    TotalPages = (int)Math.Ceiling((double)countFeedback / request.PageSize),
                },
                Rating = rating,
                TotalOfRating = countFeedback,
            }
        };
    }
}
