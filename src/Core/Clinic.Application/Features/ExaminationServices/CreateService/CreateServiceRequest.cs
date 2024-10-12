using Clinic.Application.Commons.Abstractions;
using System;

namespace Clinic.Application.Features.ExaminationServices.CreateService;

/// <summary>
///     CreateService Request
/// </summary>
public class CreateServiceRequest : IFeatureRequest<CreateServiceResponse>
{
    public string Code { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public int Price { get; init; }
    public string Group { get; init; }
}
