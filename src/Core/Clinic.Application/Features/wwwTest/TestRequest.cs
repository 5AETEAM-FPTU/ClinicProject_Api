using System;
using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Users.UpdateUserPrivateInfo;

public class TestRequest : IFeatureRequest<UpdateUserPrivateInfoResponse>
{
    public Guid UserId { get; set; }
}
