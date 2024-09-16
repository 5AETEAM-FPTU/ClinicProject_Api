using Clinic.Domain.Commons.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.MySQL.Data.EntityConfigurations;

internal sealed class PatientBookedAppointmentEntityConfiguration : IEntityTypeConfiguration<PatientBookedAppointment>
{
    public void Configure(EntityTypeBuilder<PatientBookedAppointment> builder)
    {
        builder.ToTable(
                name: $"{nameof(PatientBookedAppointment)}",
                buildAction: table => table.HasComment(comment: "Contain Patient Book Appointment records")
            );

        // Primary key configuration.
        builder.HasKey(keyExpression: patientBookedAppointment => new
        {
            patientBookedAppointment.PatientId,
            patientBookedAppointment.AppointmentId,
        });

    }
}
