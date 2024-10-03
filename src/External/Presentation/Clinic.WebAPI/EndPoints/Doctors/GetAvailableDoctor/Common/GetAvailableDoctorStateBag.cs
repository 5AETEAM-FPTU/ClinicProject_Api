using Clinic.Application.Features.Doctors.GetAvailableDoctor;

namespace Clinic.WebAPI.EndPoints.Doctors.GetAvailableDoctor.Common;

internal sealed class GetAvailableDoctorStateBag
{
    internal GetAvailableDoctorRequest AppRequest { get; } = new();
}
