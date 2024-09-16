using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clinic.WebAPI.Commons.AppCodes;
using Clinic.WebAPI.Commons.Response;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.Commons.Behaviors.Validation;

/// <summary>
///     Preprocessor for validation request.
/// </summary>
internal sealed class ValidationPreProcessor<TRequest> : PreProcessor<TRequest, ValidationStateBag>
{
    public override async Task PreProcessAsync(
        IPreProcessorContext<TRequest> context,
        ValidationStateBag state,
        CancellationToken ct
    )
    {
        if (context.HasValidationFailures)
        {
            var validationFailures = context.ValidationFailures;
            var errors = validationFailures
                .Select(selector: falure => falure.ErrorMessage)
                .ToArray();

            var httpResponse = new CommonApiResponse()
            {
                AppCode = CommonAppCode.INPUT_VALIDATION_FAIL.ToString(),
                ErrorMessages = errors,
            };

            await context.HttpContext.Response.SendAsync(
                response: httpResponse,
                statusCode: StatusCodes.Status400BadRequest,
                cancellation: ct
            );

            context.HttpContext.MarkResponseStart();
        }
    }
}
