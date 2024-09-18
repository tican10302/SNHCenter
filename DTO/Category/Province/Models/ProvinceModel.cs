using DTO.Base;
using DTO.Category.AdministrativeUnit.Dtos;

namespace DTO.Category.Province.Models;

public class ProvinceModel : ModelBase
{
    public string? Code { get; set; }
    public string? Name { get; set; }
    public string? NameEn { get; set; }
    public string? FullName { get; set; }
    public string? FullNameEn { get; set; }
    public string? CodeName { get; set; }
    public string? AdministrativeUnit { get; set; }
    public int? AdministrativeUnitId { get; set; }
    public string? AdministrativeRegion { get; set; }
    public int? AdministrativeRegionId { get; set; }
}