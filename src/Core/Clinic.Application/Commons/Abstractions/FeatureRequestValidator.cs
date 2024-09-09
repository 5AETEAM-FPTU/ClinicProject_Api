using FastEndpoints;

namespace Clinic.Application.Commons.Abstractions;

/// <summary>
///     Abstract for feature request validators.
/// </summary>
public abstract class FeatureRequestValidator<TRequest, TResponse> : Validator<TRequest>
    where TRequest : class, IFeatureRequest<TResponse>
    where TResponse : class, IFeatureResponse { }
