using DTO.Base;

namespace DTO.Category.Shift.Requests;

public class ShiftModel : ModelBase
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Time { get; set; }
    public List<string>? Days { get; set; }
}