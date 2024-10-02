using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Doctors.GetAllDoctorForBooking;

/// <summary>
///     GetAllDoctorForBooking Request
/// </summary>
public class GetAllDoctorForBookingRequest : IFeatureRequest<GetAllDoctorForBookingResponse> 
{
    public int PageIndex { get; init; } = 1;

    public int PageSize { get; init; } = 6;
}