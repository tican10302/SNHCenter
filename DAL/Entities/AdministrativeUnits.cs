using System.ComponentModel.DataAnnotations;

namespace DAL.Entities;

public class AdministrativeUnits
{
    [Key]
    public int Id { get; set; }
    public string? FullName { get; set; }
    public string? FullNameEn { get; set; }
    public string? ShortName { get; set; }
    public string? ShortNameEn { get; set; }
    public string? CodeName { get; set; }
    public string? CodeNameEn { get; set; }
}