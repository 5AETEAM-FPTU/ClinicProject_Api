using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Commons.Entities;
using FluentValidation;

namespace Clinic.Application.Features.Users.UpdatePasswordUser;

/// <summary>
///     UpdatePasswordUser request validator.
/// </summary>
public sealed class UpdatePasswordUserRequestValidator
    : FeatureRequestValidator<UpdatePasswordUserRequest, UpdatePasswordUserResponse>
{
    public UpdatePasswordUserRequestValidator()
    {
        RuleFor(expression: request => request.NewPassword)
            .NotEmpty()
            .MaximumLength(maximumLength: User.MetaData.Password.MaxLength)
            .MinimumLength(minimumLength: User.MetaData.Password.MinLength);

        RuleFor(expression: request => request.CurrentPassword)
            .NotEmpty()
            .MaximumLength(maximumLength: User.MetaData.Password.MaxLength)
            .MinimumLength(minimumLength: User.MetaData.Password.MinLength);
    }
}
