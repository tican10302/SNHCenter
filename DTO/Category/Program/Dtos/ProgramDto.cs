using System.ComponentModel.DataAnnotations;
using DTO.Base;
using FluentValidation;

namespace DTO.Category.Program.Dtos;

public class ProgramDto : DtoBase
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required")]
    public string? Name { get; set; }
}

public class ProgramDtoValidator : AbstractValidator<ProgramDto>
{
    public ProgramDtoValidator()
    {
        RuleFor(r => r.Name).NotEmpty().WithMessage("Name is not null");
    }
}