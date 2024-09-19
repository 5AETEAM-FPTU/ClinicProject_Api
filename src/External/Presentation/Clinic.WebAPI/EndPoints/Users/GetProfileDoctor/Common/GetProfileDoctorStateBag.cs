using Clinic.Application.Features.Users.GetProfileDoctor;

namespace Clinic.WebAPI.EndPoints.Users.GetProfileDoctor.Common;

internal sealed class GetProfileDoctorStateBag
{
    internal GetProfileDoctorRequest AppRequest { get; } = new();
}
