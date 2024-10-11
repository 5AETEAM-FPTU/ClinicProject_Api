using System;
using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Features.Admin.CreateMedicine;

namespace Clinic.Application.Features.ExaminationServices.CreateService;

/// <summary>
///     CreateService Response
/// </summary>
public sealed class CreateServiceResponse : IFeatureResponse
{
    public CreateServiceResponseStatusCode StatusCode { get; set; }

}

