using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace DAL.Entities;

[Index(nameof(AdministrativeUnitId), nameof(DistrictCode))]
public class Wards
{
    [Key]
    public required string Code { get; set; }
    public required string Name { get; set; }
    public string? NameEn { get; set; }
    public string? FullName { get; set; }
    public string? FullNameEn { get; set; }
    public string? CodeName { get; set; }
    public Districts? District { get; set; }
    public string? DistrictCode { get; set; }
    public AdministrativeUnits? AdministrativeUnit { get; set; }
    public int? AdministrativeUnitId { get; set; }
}