using FluentValidation;

namespace DTO.System.Account.Dtos;

public class AccountDto
{
    public string? UserName { get; set; }
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