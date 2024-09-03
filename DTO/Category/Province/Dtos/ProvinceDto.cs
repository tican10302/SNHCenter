namespace DTO.Category.Province.Dtos;

public class ProvinceDto
{
    public string? Code { get; set; }
    public string? Name { get; set; }
    public string? NameEn { get; set; }
    public required string FullName { get; set; }
    public string? FullNameEn { get; set; }
    public string? CodeName { get; set; }
    public int? AdministrativeUnitId { get; set; }
    public int? AdministrativeRegionId { get; set; }
}