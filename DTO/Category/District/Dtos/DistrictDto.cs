using DTO.Category.AdministrativeUnit.Dtos;

namespace DTO.Category.District.Dtos;

public class DistrictDto
{
    public string? Code { get; set; }
    public string? Name { get; set; }
    public string? NameEn { get; set; }
    public string? FullName { get; set; }
    public string? FullNameEn { get; set; }
    public string? CodeName { get; set; }
    public int? AdministrativeUnitId { get; set; }
    public string? ProvinceCode { get; set; }
}