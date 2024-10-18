using System;
using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Doctors.GetUserInforById;

/// <summary>
///     GetUserInforById Request
/// </summary>
public class GetUserInforByIdRequest : IFeatureRequest<GetUserInforByIdResponse>
{
    public Guid UserId { get; init; }
}
