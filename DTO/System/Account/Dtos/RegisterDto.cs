using FluentValidation;

namespace DTO.System.Account.Dtos;

public class RegisterDto
{
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public string? PasswordConfirm { get; set; }
}

public class RegisterDtoValidator : AbstractValidator<RegisterDto>
{
    public RegisterDtoValidator()
    {
        RuleFor(r => r.UserName).NotEmpty().WithMessage("Username is not null");
        RuleFor(r => r.UserName).MinimumLength(4).WithMessage("Username must be greater than 4 characters");
        RuleFor(r => r.Password).NotEmpty().WithMessage("Password is not null");
        RuleFor(r => r.Password).MinimumLength(6).WithMessage("Password must be greater than 6 characters");
        RuleFor(r => r.PasswordConfirm).NotEmpty().WithMessage("Confirm password is not null");
        RuleFor(r => r.Password).Equal(r => r.PasswordConfirm).WithMessage("Password is not equal");
    }
}