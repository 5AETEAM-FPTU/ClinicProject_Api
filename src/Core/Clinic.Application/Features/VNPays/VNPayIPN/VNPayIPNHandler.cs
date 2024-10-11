using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Commons.Constance;
using Clinic.Application.Commons.VNPay.Response;
using Clinic.Application.Features.VNPays.VNPayIPN;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Clinic.Application.Features.Users.VNPays.VNPayIPN;

/// <summary>
///     GetAllDoctor Handler
/// </summary>
public class VNPayIPNHandler : IFeatureHandler<VNPayIPNRequest, VnpayPayIpnResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly UserManager<User> _userManager;    
    public VNPayIPNHandler(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor, UserManager<User> userManager)
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
    public async Task<VnpayPayIpnResponse> ExecuteAsync(
        VNPayIPNRequest request,
        CancellationToken cancellationToken
    )
    {
        var userId = _contextAccessor.HttpContext.User.FindFirstValue(claimType: "sub");

        VnpayPayResponse vnPayPayResponse = new VnpayPayResponse(
                request.vnp_TmnCode,
                request.vnp_BankCode,
                request.vnp_BankTranNo,
                request.vnp_CardType,
                request.vnp_OrderInfo,
                request.vnp_TransactionNo,
                request.vnp_TransactionStatus,
                request.vnp_TxnRef,
                request.vnp_SecureHashType,
                request.vnp_SecureHash,
                request.vnp_Amount,
                request.vnp_ResponseCode,
                request.vnp_PayDate,
                request.appointmentId
        );


        var isValidSignature = vnPayPayResponse.IsValidSignature("V37IL0UDV6RS5CTZ1BZFYH0UM3WJU5T1"); // hashsecure in config

        // check appointmentId exist

        // if valid signature
        if (isValidSignature)
        {
            if (request.vnp_ResponseCode == "00" && request.vnp_TransactionStatus == "00")
            {
                // call repository save online payment
                var newOnlinePaymentInfo = new OnlinePayment()
                {
                    Id = Guid.Parse(request.vnp_TxnRef),            // la vnp_txnRef
                    AppointmentId = Guid.Parse(request.appointmentId),
                    Amount = (int)request.vnp_Amount,
                    TransactionID = request.vnp_BankTranNo,
                    PaymentMethod = "VNPAY",
                    CreatedBy = Guid.Parse(userId),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = CommonConstant.MIN_DATE_TIME,
                    UpdatedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
                };

                var dbResult =
                    await _unitOfWork.CreateNewOnlinePaymentRepository.CreateNewOnlinePaymentAsync(
                        onlinePayment: newOnlinePaymentInfo,
                        cancellationToken: cancellationToken
                    );

                if(!dbResult)
                {
                    return new VnpayPayIpnResponse()
                    {
                        ResponseBody = new()
                        {
                            RspCode = "99",
                            Message = "Input required data"
                        }
                    };
                } 
        
            } else
            {
                return new VnpayPayIpnResponse()
                {
                    ResponseBody = new()
                    {
                        RspCode = "99",
                        Message = "Input required data"
                    }
                };
            }

        }

        return new VnpayPayIpnResponse()
        {
            ResponseBody = new()
            {
                RspCode = "00",
                Message = "Confirm Success"
            }
        };


    }
}
