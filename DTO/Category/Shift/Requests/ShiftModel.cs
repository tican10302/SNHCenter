using DTO.Base;

namespace DTO.Category.Shift.Requests;

public class ShiftModel : ModelBase
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public TimeSpan? Time { get; set; }
    public string? Days { get; set; }
    public List<string>? SelectDays { get; set; }
}