using DTO.Base;

namespace DTO.System.Role.Models;

public class RoleModel : ModelBase
{
    public int RoleCode { get; set; }
    public string? Name { get; set; }
}