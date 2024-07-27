namespace DTO.System.Account.Requests;

public class AccountPermissionModel
{
    public AccountModel? Account { get; set; }
    public List<PermissionModel>? Permission { get; set; }
}