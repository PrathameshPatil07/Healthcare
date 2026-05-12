using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hospital.API.Models;

public partial class Hospital
{
    [Key]
    public int HospitalId { get; set; }

    [StringLength(200)]
    public string HospitalName { get; set; } = null!;

    [StringLength(300)]
    public string? Address { get; set; }

    [StringLength(15)]
    public string? PhoneNumber { get; set; }

    [StringLength(100)]
    public string? Email { get; set; }

    [StringLength(100)]
    public string? City { get; set; }

    [StringLength(100)]
    public string? StateName { get; set; }

    [StringLength(10)]
    public string? Pincode { get; set; }

    public bool? IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }
}
