//using Clinic.Application.Commons.Abstractions;
//using Clinic.Application.Features.Users.GetAllDoctor;
//using Clinic.Domain.Features.UnitOfWorks;
//using System.Threading.Tasks;
//using System.Threading;
//using Microsoft.AspNetCore.Identity;
//using Clinic.Domain.Commons.Entities;

//namespace Clinic.Application.Features.Users.AddDoctor;

///// <summary>
/////     GetAllDoctor Handler
///// </summary>
//public class AddDoctorHandler : IFeatureHandler<AddDoctorRequest, AddDoctorResponse>
//{
//    private readonly IUnitOfWork _unitOfWork;
//    private readonly UserManager<User> _userManager;
//    public AddDoctorHandler(IUnitOfWork unitOfWork, UserManager<User> userManager)
//    {
//        _unitOfWork = unitOfWork;
//        _userManager = userManager;
//    }

//    /// <summary>
//    ///     Entry of new request handler.
//    /// </summary>
//    /// <param name="request">
//    ///     Request model.
//    /// </param>
//    /// <param name="ct">
//    ///     A token that is used for notifying system
//    ///     to cancel the current operation when user stop
//    ///     the request.
//    /// </param>
//    /// <returns>
//    ///     A task containing the response.
//    public async Task<AddDoctorResponse> ExecuteAsync(
//        AddDoctorRequest request,
//        CancellationToken cancellationToken
//    )
//    {
//        // Find user by email.
//        var user = await _userManager.FindByEmailAsync(request.Email);

//        // If not found, response.
//        if ( user != null )
//        { return new AddDoctorResponse()
//        {
//            StatusCode = AddDoctorResponseStatusCode.EMAIL_DOCTOR_EXITS
//        }; }
//        // Is Guid found.

//        // Response if it not found.

//        // Create User instance.

//        // Response if database operation failed.

//        // Response successfully.
//        return new AddDoctorResponse()
//        {
//            StatusCode = AddDoctorResponseStatusCode.OPERATION_SUCCESS,

//        };
//    }
//}
