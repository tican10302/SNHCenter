using DTO.System.Role.Models;

namespace DTO.System.Account.Dtos;

public class PermissionDto
{
    public RoleModel? Role { get; set; }
    public string? ControllerName { get; set; }
    public bool IsView { get; set; }
    public bool IsAdd { get; set; }
    public bool IsEdit { get; set; }
    public bool IsDelete { get; set; }
    public bool IsApprove { get; set; }
    public bool IsStatistic { get; set; }
}