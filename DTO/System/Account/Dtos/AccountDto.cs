using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace DTO.System.Account.Dtos;

public class AccountDto
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Username is required")]
    public string? UserName { get; set; }
    [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
    public string? Password { get; set; }
}

public class AccountDtoValidator : AbstractValidator<AccountDto>
{
    public AccountDtoValidator()
    {
        RuleFor(r => r.UserName).NotEmpty().WithMessage("Username is not null");
        RuleFor(r => r.Password).NotEmpty().WithMessage("Password is not null");
    }
}