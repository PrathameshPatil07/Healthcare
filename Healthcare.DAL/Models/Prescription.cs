using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hospital.API.Models;

public partial class Prescription
{
    [Key]
    public int PrescriptionId { get; set; }

    public int AppointmentId { get; set; }

    public int DoctorId { get; set; }

    public int PatientId { get; set; }

    public string? Notes { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [ForeignKey("AppointmentId")]
    [InverseProperty("Prescriptions")]
    public virtual Appointment Appointment { get; set; } = null!;

    [ForeignKey("DoctorId")]
    [InverseProperty("Prescriptions")]
    public virtual Doctor Doctor { get; set; } = null!;

    [ForeignKey("PatientId")]
    [InverseProperty("Prescriptions")]
    public virtual Patient Patient { get; set; } = null!;
}
