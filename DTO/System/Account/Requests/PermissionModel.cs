namespace DTO.System.Account.Requests;

public class PermissionModel
{
    public RoleModel? Role { get; set; }
    public string? ControllerName { get; set; }
    public bool IsView { get; set; }
    public bool IsAdd { get; set; }
    public bool IsEdit { get; set; }
    public bool IsDelete { get; set; }
}