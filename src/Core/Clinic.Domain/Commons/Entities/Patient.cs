﻿using Clinic.Domain.Commons.Entities.Base;
using System;
using System.Collections.Generic;


namespace Clinic.Domain.Commons.Entities;

/// <summary>
///     Represent the "Patients" table.
/// </summary>
public class Patient : IBaseEntity
{
    // Primary keys.
    // Foreign keys.
    public Guid Id { get; set; }

    // Foreign keys
    public Guid UserId { get; set; }

    // Normal columns.
    public string Gender { get; set; } 

    public DateTime DOB { get; set; }

    public string Adress { get; set; }  

    public string Description {  get; set; }

    // Navigation properties.
    public IEnumerable<OnlinePayment> OnlinePayments { get; set; }
    public IEnumerable<Appointment> Appointments { get; set; }
    public IEnumerable<PatientBookedAppointment> PatientBookAppointments { get; set; }
    public IEnumerable<MedicalReport> MedicalReports { get; set; }
    public IEnumerable<QueueRoom> QueueRooms { get; set; }
    public IEnumerable<ChatRoom> ChatRooms { get; set; }   
    public User User { get; set; }
    
}

