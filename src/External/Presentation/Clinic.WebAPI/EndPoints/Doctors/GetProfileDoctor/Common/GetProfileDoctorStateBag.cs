using Clinic.Application.Features.Doctors.GetProfileDoctor;

namespace Clinic.WebAPI.EndPoints.Doctors.GetProfileDoctor.Common;

internal sealed class GetProfileDoctorStateBag
{
    internal GetProfileDoctorRequest AppRequest { get; } = new();
}
