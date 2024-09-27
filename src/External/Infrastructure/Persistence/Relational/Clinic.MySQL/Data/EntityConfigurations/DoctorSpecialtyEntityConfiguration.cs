using Clinic.Domain.Commons.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic.MySQL.Data.EntityConfigurations;

internal sealed class DoctorSpecialtyEntityConfiguration : IEntityTypeConfiguration<DoctorSpecialty>
{
    public void Configure(EntityTypeBuilder<DoctorSpecialty> builder)
    {
        builder.ToTable(
            name: $"DoctorSpecialties",
            buildAction: table => table.HasComment(comment: "Contain Doctor Specialty record")
        );

        // Primary key configuration.
        builder.HasKey(keyExpression: serviceOrderItem => new
        {
            serviceOrderItem.DoctorId,
            serviceOrderItem.SpecialtyID,
        });
    }
}
