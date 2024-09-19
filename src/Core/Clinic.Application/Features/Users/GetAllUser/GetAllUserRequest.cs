using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Users.GetAllUser;

/// <summary>
///     GetAllDoctors Request
/// </summary>
public class GetAllUserRequest : IFeatureRequest<GetAllUserResponse>
{
    public int PageIndex { get; init; } = 1;

    public int PageSize { get; init; } = 10;
}
