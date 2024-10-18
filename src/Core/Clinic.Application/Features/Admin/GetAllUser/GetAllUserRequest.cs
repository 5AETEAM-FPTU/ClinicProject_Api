using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Admin.GetAllUser;

/// <summary>
///     GetAllDoctors Request
/// </summary>
public class GetAllUserRequest : IFeatureRequest<GetAllUserResponse>
{
    public int PageIndex { get; init; } 

    public int PageSize { get; init; } 
}
