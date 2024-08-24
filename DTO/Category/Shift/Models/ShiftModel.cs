using DTO.Base;

namespace DTO.Category.Shift.Models;

public class ShiftModel : ModelBase
{
    public string? Name { get; set; }
    public TimeSpan? Time { get; set; }
    public string? Days { get; set; }
    public List<string>? SelectDays { get; set; }
}