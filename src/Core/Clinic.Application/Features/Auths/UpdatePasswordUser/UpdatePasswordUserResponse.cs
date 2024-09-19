using System;
using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Auths.UpdatePasswordUser;

/// <summary>
///     UpdatePasswordUser Response
/// </summary>
public class UpdatePasswordUserResponse : IFeatureResponse
{
    public UpdatePasswordUserResponseStatusCode StatusCode { get; init; }
}
