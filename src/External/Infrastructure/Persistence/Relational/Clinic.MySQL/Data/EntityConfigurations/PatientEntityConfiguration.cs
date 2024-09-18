using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clinic.Application.Commons.Constance;
using Clinic.Domain.Commons.Entities;
using Clinic.MySQL.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CommonConstant = Clinic.MySQL.Common.CommonConstant;

namespace Clinic.MySQL.Data.EntityConfigurations;

internal sealed class PatientEntityConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.ToTable(
            name: $"{nameof(Patient)}s",
            buildAction: table => table.HasComment(comment: "Contain patient's infomation.")
        );

        // Primary key configuration.
        builder.HasKey(keyExpression: patient => patient.Id);

        /* Properties configuration */
        // Description
        builder
            .Property(propertyExpression: patient => patient.Description)
            .HasColumnType(typeName: CommonConstant.Database.DataType.TEXT);

        // DOB
        builder
            .Property(propertyExpression: patient => patient.DOB)
            .HasColumnType(typeName: CommonConstant.Database.DataType.DATETIME);

        // Address
        builder
            .Property(propertyExpression: patient => patient.Address)
            .HasColumnType(typeName: CommonConstant.Database.DataType.VarcharGenerator.Get(225))

        // Table relationships configurations.
        // [Patients] - [MedicalReports] (1 - n).
        builder
            .HasMany(navigationExpression: patient => patient.MedicalReports)
            .WithOne(navigationExpression: medicalReport => medicalReport.Patient)
            .HasForeignKey(foreignKeyExpression: medicalReport => medicalReport.PatientId)
            .IsRequired();

        // [Patients] - [Appointments] (1 - n).
        builder
            .HasMany(navigationExpression: patient => patient.Appointments)
            .WithOne(navigationExpression: appointment => appointment.Patient)
            .HasForeignKey(foreignKeyExpression: appointment => appointment.PatientId)
            .IsRequired();

        // [Patients] - [OnlinePayments] (1 - n).
        builder
            .HasMany(navigationExpression: patient => patient.OnlinePayments)
            .WithOne(navigationExpression: onlinePayment => onlinePayment.Patient)
            .HasForeignKey(foreignKeyExpression: onlinePayment => onlinePayment.PatientId)
            .IsRequired();

        // [Patients] - [PatientBookedAppointment] (1 - n).
        builder
            .HasMany(navigationExpression: patient => patient.PatientBookAppointments)
            .WithOne(navigationExpression: patientBookAppointment => patientBookAppointment.Patient)
            .HasForeignKey(foreignKeyExpression: patientBookAppointment =>
                patientBookAppointment.PatientId
            )
            .IsRequired();

        // [Patients] - [QueueRoom] (1 - n).
        builder
            .HasMany(navigationExpression: patient => patient.QueueRooms)
            .WithOne(navigationExpression: queueRoom => queueRoom.Patient)
            .HasForeignKey(foreignKeyExpression: queueRoom => queueRoom.PatientId)
            .IsRequired();

        // [Patients] - [ChatRoom] (1 - n).
        builder
            .HasMany(navigationExpression: patient => patient.ChatRooms)
            .WithOne(navigationExpression: chatRoom => chatRoom.Patient)
            .HasForeignKey(foreignKeyExpression: chatRoom => chatRoom.PatientId)
            .IsRequired();
    }
}
