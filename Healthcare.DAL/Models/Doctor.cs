using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hospital.API.Models;

public partial class Doctor
{
    [Key]
    public int DoctorId { get; set; }

    public int UserId { get; set; }

    public int DepartmentId { get; set; }

    [StringLength(100)]
    public string FullName { get; set; } = null!;

    [StringLength(100)]
    public string? Specialization { get; set; }

    [StringLength(200)]
    public string? Qualification { get; set; }

    public int? ExperienceYears { get; set; }

    [StringLength(15)]
    public string? PhoneNumber { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? ConsultationFees { get; set; }

    public bool? IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [InverseProperty("Doctor")]
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    [ForeignKey("DepartmentId")]
    [InverseProperty("Doctors")]
    public virtual Department Department { get; set; } = null!;

    [InverseProperty("Doctor")]
    public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();

    [ForeignKey("UserId")]
    [InverseProperty("Doctors")]
    public virtual User User { get; set; } = null!;
}
