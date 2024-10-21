using Clinic.Application.Features.Doctors.GetIdsDoctor;

namespace Clinic.WebAPI.EndPoints.Doctors.GetIdsDoctor.Common;

/// <summary>
/// Get ids doctor state bag
/// </summary>
internal sealed class GetIdsDoctorStateBag
{
    internal GetIdsDoctorRequest AppRequest { get; } = new();
}
