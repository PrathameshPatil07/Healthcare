using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hospital.API.Models;

public partial class MedicalRecord
{
    [Key]
    public int RecordId { get; set; }

    public int PatientId { get; set; }

    public string? Diagnosis { get; set; }

    public string? Treatment { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? RecordDate { get; set; }

    [ForeignKey("PatientId")]
    [InverseProperty("MedicalRecords")]
    public virtual Patient Patient { get; set; } = null!;
}
