using System;
using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Features.Admin.CreateMedicine;
using Clinic.Application.Features.ExaminationServices.CreateService;

namespace Clinic.Application.Features.ExaminationServices.UpdateService;

/// <summary>
///     UpdateService Response
/// </summary>
public sealed class UpdateServiceResponse : IFeatureResponse
{
    public UpdateServiceResponseStatusCode StatusCode { get; set; }

}

