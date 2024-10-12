using Clinic.Application.Commons.Abstractions;
using FastEndpoints;

namespace Clinic.Application.Features.ExaminationServices.GetAllServices;

/// <summary>
///     GetAllServices Request
/// </summary>
public class GetAllServicesRequest : IFeatureRequest<GetAllServicesResponse>
{
    public int PageIndex { get; init; } = 1;

    public int PageSize { get; init; } = 10;

    [BindFrom("key")]
    public string? CodeOrName { get; init; } = "";

}
