using DTO.Base;

namespace DTO.System.Role.Dtos;

public class RoleDto : DtoBase
{
    public int RoleCode { get; set; }
    public string? Name { get; set; }
}