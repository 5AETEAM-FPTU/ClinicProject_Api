
using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Users.UpdateUserDesciption;

/// <summary>
///     GetProfileUser Response
/// </summary>
public class UpdateUserDesciptionResponse : IFeatureResponse
{
    public UpdateUserDesciptionResponseStatusCode StatusCode { get; init; }

}
