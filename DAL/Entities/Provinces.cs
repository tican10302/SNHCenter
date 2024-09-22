using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace DAL.Entities;

[Index(nameof(AdministrativeUnitId), nameof(AdministrativeRegionId))]
public class Provinces : EntitiesBase
{
    [Key]
    public required string Code { get; set; }

    public required string Name { get; set; }
    public string? NameEn { get; set; }
    public required string FullName { get; set; }
    public string? FullNameEn { get; set; }
    public string? CodeName { get; set; }
    public AdministrativeUnits? AdministrativeUnit { get; set; }
    public int? AdministrativeUnitId { get; set; }
    public AdministrativeRegions? AdministrativeRegion { get; set; }
    public int? AdministrativeRegionId { get; set; }
}