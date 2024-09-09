using Clinic.Application.Commons.Abstractions;
using FluentValidation;

namespace Clinic.Application.Features.HelloWorld;

/// <summary>
///     HelloWorld request validator.
/// </summary>
public sealed class HelloWorldRequestValidator
    : FeatureRequestValidator<HelloWorldRequest, HelloWorldResponse>
{
    public HelloWorldRequestValidator()
    {
        RuleFor(expression: request => request.FirstName)
            .NotEmpty()
            .MaximumLength(maximumLength: 20)
            .MinimumLength(minimumLength: 3);

        RuleFor(expression: request => request.LastName)
            .NotEmpty()
            .MaximumLength(maximumLength: 20)
            .MinimumLength(minimumLength: 3);
    }
}
