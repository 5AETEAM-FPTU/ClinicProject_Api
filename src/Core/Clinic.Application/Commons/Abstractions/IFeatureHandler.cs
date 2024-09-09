using FastEndpoints;

namespace Clinic.Application.Commons.Abstractions;

/// <summary>
///     Interface for handling feature requests.
/// </summary>
public interface IFeatureHandler<TRequest, TResponse> : ICommandHandler<TRequest, TResponse>
    where TRequest : class, IFeatureRequest<TResponse>
    where TResponse : class, IFeatureResponse { }
