using Clinic.Application.Commons.Abstractions;
using FluentValidation;

namespace Clinic.Application.Features.Users.UpdateUserPrivateInfo;

public sealed class UpdateUserPrivateInfoRequestValidator
    : FeatureRequestValidator<UpdateUserPrivateInfoRequest, UpdateUserPrivateInfoResponse>
{
    public UpdateUserPrivateInfoRequestValidator()
    {
        RuleFor(x => x.PhoneNumber)
        .Matches(@"^\d{10,15}$") // Chỉ chấp nhận các số có độ dài từ 10 đến 15 chữ số
        .When(x => !string.IsNullOrEmpty(x.PhoneNumber)) // Chỉ kiểm tra khi PhoneNumber không rỗng
        .WithMessage("Phone number must be between 10 and 15 digits and cannot start with a '+'.");
    }
}
