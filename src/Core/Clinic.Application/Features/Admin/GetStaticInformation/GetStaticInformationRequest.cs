using Clinic.Application.Commons.Abstractions;
using System;

namespace Clinic.Application.Features.Admin.GetStaticInformation;

/// <summary>
///     GetStaticInformation Request
/// </summary>
public class GetStaticInformationRequest : IFeatureRequest<GetStaticInformationResponse>{
    public int year { get; init; }
}
