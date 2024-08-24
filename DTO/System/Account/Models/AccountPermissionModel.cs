namespace DTO.System.Account.Models;

public class AccountPermissionModel
{
    public AccountModel? Account { get; set; }
    public List<PermissionModel>? Permission { get; set; }
    public List<MenuModel>? Menu { get; set; }
    public List<GroupPermissionModel>? GroupPermission { get; set; }
}