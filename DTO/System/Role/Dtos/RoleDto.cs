using DTO.Base;
using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace DTO.System.Role.Dtos;

public class RoleDto : DtoBase
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Role code is required")]
    public int RoleCode { get; set; }
    [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required")]
    public string? Name { get; set; }
}

public class RoleDtoValidator : AbstractValidator<RoleDto>
{
    public RoleDtoValidator()
    {
        RuleFor(r => r.RoleCode).NotEmpty().WithMessage("Role code is required");
        RuleFor(r => r.Name).NotEmpty().WithMessage("Name is required");
    }
}