using System.ComponentModel.DataAnnotations;

namespace DAL.Entities;

public class AdministrativeRegions
{
    [Key]
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string NameEn { get; set; }
    public string? CodeName { get; set; }
    public string? CodeNameEn { get; set; }
}