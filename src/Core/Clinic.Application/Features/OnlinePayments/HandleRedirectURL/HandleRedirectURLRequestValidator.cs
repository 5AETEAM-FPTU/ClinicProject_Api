using Clinic.Application.Commons.Abstractions;
using FluentValidation;

namespace Clinic.Application.Features.OnlinePayments.HandleRedirectURL;

/// <summary>
///     HandleRedirectURL Request Validator.
/// </summary>
public sealed class HandleRedirectURLRequestValidator
    : FeatureRequestValidator<HandleRedirectURLRequest, HandleRedirectURLResponse>
{
    public HandleRedirectURLRequestValidator()
    {
        RuleFor(x => x.Amount).NotEmpty();

        RuleFor(x => x.BankCode).NotEmpty().WithMessage("Bank code is required.");

        RuleFor(x => x.BankTransactionNo)
            .NotEmpty()
            .WithMessage("Bank transaction number is required.");

        RuleFor(x => x.CardType).NotEmpty().WithMessage("Card type is required.");

        RuleFor(x => x.OrderInfo).NotEmpty().WithMessage("Order information is required.");

        RuleFor(x => x.PayDate)
            .NotEmpty()
            .WithMessage("Payment date is required.")
            .Matches(@"^\d{14}$")
            .WithMessage("Pay date must be in the format YYYYMMDDHHMMSS.");

        RuleFor(x => x.ResponseCode).NotEmpty().WithMessage("Response code is required.");

        RuleFor(x => x.TerminalCode).NotEmpty().WithMessage("Terminal code is required.");

        RuleFor(x => x.TransactionNo).NotEmpty().WithMessage("Transaction number is required.");

        RuleFor(x => x.TransactionStatus).NotEmpty().WithMessage("Transaction status is required.");

        RuleFor(x => x.TransactionRef).NotEmpty().WithMessage("Transaction reference is required.");

        RuleFor(x => x.SecureHash).NotEmpty().WithMessage("Secure hash is required.");
    }
}
