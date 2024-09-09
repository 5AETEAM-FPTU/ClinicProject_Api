using FastEndpoints;

namespace Clinic.Application.Commons.Abstractions;

/// <summary>
///     Marker interface to represent a request with a response
/// </summary>
public interface IFeatureRequest<out TResponse> : ICommand<TResponse>
    where TResponse : class, IFeatureResponse { }
