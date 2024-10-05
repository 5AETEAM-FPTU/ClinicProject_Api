using Clinic.Application.Commons.Abstractions;
using FastEndpoints;
using System;

namespace Clinic.Application.Features.Doctors.GetAllDoctorForBooking;

/// <summary>
///     GetAllDoctorForBooking Request
/// </summary>
public class GetAllDoctorForBookingRequest : IFeatureRequest<GetAllDoctorForBookingResponse> 
{
    public int PageIndex { get; init; } = 1;

    public int PageSize { get; init; } = 6;

    [BindFrom("doctorName")]
    public string? Name { get; init; }
    [BindFrom("doctorGender")]
    public Guid? GenderId { get; init; }
    [BindFrom("doctorSpecialtyId")]
    public Guid? SpecialtyId { get; init; }
}