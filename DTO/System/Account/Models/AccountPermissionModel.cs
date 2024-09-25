using DTO.System.Menu.Models;

namespace DTO.System.Account.Models;

public class AccountPermissionModel
{
    public AccountModel? Account { get; set; }
    public List<PermissionModel>? Permission { get; set; }
    public List<MenuModel>? Menu { get; set; }
    public List<GroupPermissionModel>? GroupPermission { get; set; }
    public string? UserName { get; set; }
    public string? Token { get; set; }
}