﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;

namespace Clinic.Domain.Features.Repositories.MedicalReports.UpdateMainMedicalReportInformation;

public interface IUpdateMainMedicalReportInformationRepository
{
    Task<MedicalReport> GetMedicalReportsByIdQueryAsync(
        Guid medicalReportId,
        CancellationToken cancellationToken
    );

    Task<bool> UpdateMainMedicalReportInformationCommandAsync(
        MedicalReport medicalReport,
        CancellationToken cancellationToken
    );
}
