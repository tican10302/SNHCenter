using DTO.Base;

namespace DTO.System.Account.Models;

public class MenuModel : ModelBase
{
    public string? ControllerName { get; set; }
    public string? Controller { get; set; }
    public string? Action { get; set; }
    public string? Name { get; set; }
    public Guid GroupPermissionId { get; set; }
    public string? GroupName { get; set; }
    public int GroupSort { get; set; }
    public bool IsView { get; set; } = false;
    public bool IsAdd { get; set; } = false;
    // public bool IsEdit { get; set; } = false;
    public bool IsDelete { get; set; } = false;
    public bool IsApprove { get; set; } = false;
    public bool IsStatistic { get; set; } = false;
    public bool IsShowMenu { get; set; } = true;
}