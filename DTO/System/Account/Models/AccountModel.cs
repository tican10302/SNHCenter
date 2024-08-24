namespace DTO.System.Account.Models;

public class AccountModel
{
    public Guid Id { get; set; }
    public string? UserName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Role { get; set; }
    public string? Avatar { get; set; }
    public string? Token { get; set; }
}