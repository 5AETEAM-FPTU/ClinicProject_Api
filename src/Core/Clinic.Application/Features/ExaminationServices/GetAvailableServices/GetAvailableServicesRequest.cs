using Clinic.Application.Commons.Abstractions;
using FastEndpoints;

namespace Clinic.Application.Features.ExaminationServices.GetAvailableServices;

/// <summary>
///     GetAvailableServices Request
/// </summary>
public class GetAvailableServicesRequest : IFeatureRequest<GetAvailableServicesResponse>
{
    [BindFrom("key")]
    public string CodeOrName { get; init; } = "";
}
