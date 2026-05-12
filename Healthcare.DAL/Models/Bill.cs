using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hospital.API.Models;

public partial class Bill
{
    [Key]
    public int BillId { get; set; }

    public int PatientId { get; set; }

    public int AppointmentId { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? TotalAmount { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? PaidAmount { get; set; }

    [StringLength(20)]
    public string? PaymentStatus { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [ForeignKey("AppointmentId")]
    [InverseProperty("Bills")]
    public virtual Appointment Appointment { get; set; } = null!;

    [ForeignKey("PatientId")]
    [InverseProperty("Bills")]
    public virtual Patient Patient { get; set; } = null!;
}
