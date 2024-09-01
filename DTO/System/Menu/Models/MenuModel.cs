using DTO.Base;

namespace DTO.System.Menu.Models;

public class MenuModel : ModelBase
{
    public string? ControllerName { get; set; }
    public string? Controller { get; set; }
    public string? Action { get; set; }
    public string? Name { get; set; }
    public string? Icon { get; set; }
    public Guid GroupPermissionId { get; set; }
    public string? GroupName { get; set; }
    public int GroupSort { get; set; }
    public bool HasView { get; set; } = false;
    public bool HasAdd { get; set; } = false;
    public bool HasEdit { get; set; } = false;
    public bool HasDelete { get; set; } = false;
    public bool HasApprove { get; set; } = false;
    public bool HasStatistic { get; set; } = false;
    public bool IsShowMenu { get; set; } = true;
}