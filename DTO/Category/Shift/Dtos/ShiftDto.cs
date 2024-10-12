using System.ComponentModel.DataAnnotations;
using DTO.Base;
using FluentValidation;

namespace DTO.Category.Shift.Dtos;

public class ShiftDto : DtoBase
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required")]
    public string? Name { get; set; }
    [Required(AllowEmptyStrings = false, ErrorMessage = "Time is required")]
    public TimeSpan? Time { get; set; }
    [Required(AllowEmptyStrings = false, ErrorMessage = "Days is required")]
    public string? Days { get; set; }
    public string? Note { get; set; }
}

public class ShiftDtoValidator : AbstractValidator<ShiftDto>
{
    public ShiftDtoValidator()
    {
        RuleFor(r => r.Name).NotEmpty().WithMessage("Name is not null");
        RuleFor(r => r.Time).NotEmpty().WithMessage("Time is not null");
        RuleFor(r => r.Days).NotEmpty().WithMessage("Days is not null");
    }
}