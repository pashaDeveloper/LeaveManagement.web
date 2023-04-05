using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using LeaveManagement.web.Data.Attributes;
using Microsoft.AspNetCore.Identity;

namespace LeaveManagement.web.Areas.Identity.Data;

[AuditTable]
public class Employee : IdentityUser
{
    [PersonalData]
    [DataType(DataType.Text)]
    [Column(TypeName ="nvarchar(100)")]
    public string? FirsName { get; set; }
    [PersonalData]
    [DataType(DataType.Text)]
    [Column(TypeName = "nvarchar(100)")]
    public string? LastName { get; set; }
    [PersonalData]
    [DataType(DataType.Text)]
    [Column(TypeName = "nvarchar(100)")]
    public string TaxId { get; set; }
    [PersonalData]
    [DataType(DataType.DateTime)]
    [Column(TypeName = "nvarchar(100)")]
    public DateTime DateOfBirth { get; set; }
    [PersonalData]
    [DataType(DataType.DateTime)]
    [Column(TypeName = "nvarchar(100)")]
    public DateTime DateJoined { get; set; }
}

