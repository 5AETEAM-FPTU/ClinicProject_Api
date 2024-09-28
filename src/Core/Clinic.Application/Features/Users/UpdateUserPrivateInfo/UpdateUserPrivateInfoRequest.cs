using System;
using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Users.UpdateUserPrivateInfo;

/// <summary>
///     GetProfileUser Request
/// </summary>
public class UpdateUserPrivateInfoRequest : IFeatureRequest<UpdateUserPrivateInfoResponse>
{
    public string FullName { get; set; }
    public Guid GenderId { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime DOB { get; set; }
    public string Address { get; set; }
}
