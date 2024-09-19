using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Users.UpdateUserDesciption;

/// <summary>
///     GetProfileUser Request
/// </summary>
public class UpdateUserDesciptionRequest : IFeatureRequest<UpdateUserDesciptionResponse> 
{
    public string Description { get; set; }

}
