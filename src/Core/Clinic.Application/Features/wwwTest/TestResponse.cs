using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Users.UpdateUserPrivateInfo;

public class TestResponse : IFeatureResponse
{
    public string Token { get; init; }
}
