using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Commons.VNPay.Request;
using Clinic.Application.Features.VNPays.CreatePaymentLink;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Clinic.Application.Features.Users.VNPays.CreatePaymentLink;

/// <summary>
///     GetAllDoctor Handler
/// </summary>
public class CreatePaymentLinkHandler : IFeatureHandler<CreatePaymentLinkRequest, CreatePaymentLinkResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly UserManager<User> _userManager;    
    public CreatePaymentLinkHandler(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor, UserManager<User> userManager)
    {
        _unitOfWork = unitOfWork;
        _contextAccessor = contextAccessor;
        _userManager = userManager;
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
    public async Task<CreatePaymentLinkResponse> ExecuteAsync(
        CreatePaymentLinkRequest request,
        CancellationToken cancellationToken
    )
    {
        // Get userId from sub type jwt
        var userId = _contextAccessor.HttpContext.User.FindFirstValue(claimType: JwtRegisteredClaimNames.Sub);
        
        // Get ip address
        var ipAddress = _contextAccessor?.HttpContext?.Connection?.LocalIpAddress?.ToString();

        // tạo vnpay url
        // them appointment id va userId
        var vnpayPayRequest = new VnpayPayRequest(
                                    vnp_IpAddr: ipAddress,
                                    vnp_Amount: request.Amount,
                                    vnp_OrderInfo: request.Description,
                                    createdDate: DateTime.Now,
                                    vnp_TxnRef: Guid.NewGuid().ToString(),
                                    userId: userId,
                                    appointmentId: request.SlotId.ToString()
                                    );

        var paymentUrl = vnpayPayRequest.GetLink("", "V37IL0UDV6RS5CTZ1BZFYH0UM3WJU5T1");  //hashsecure in config
        
        return new CreatePaymentLinkResponse()
        {
            StatusCode = CreatePaymentLinkResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new()
            {
                PaymentUrl = paymentUrl,
            }
        };
    }

}
