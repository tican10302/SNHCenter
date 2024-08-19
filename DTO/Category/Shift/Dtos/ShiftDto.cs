using System.ComponentModel.DataAnnotations;
using DTO.Base;
using FluentValidation;

namespace DTO.Category.Shift.Dtos;

public class ShiftDto : BaseRequest
{
    public Guid Id { get; set; }
    [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required")]
    public string? Name { get; set; }
    [Required(AllowEmptyStrings = false, ErrorMessage = "Time is required")]
    public TimeSpan? Time { get; set; }
    public string? Days { get; set; }
    [Required(AllowEmptyStrings = false, ErrorMessage = "Days is required")]
    public List<string>? SelectDays { get; set; }
}

public class ShiftDtoValidator : AbstractValidator<ShiftDto>
{
    public ShiftDtoValidator()
    {
        RuleFor(r => r.Name).NotEmpty().WithMessage("Name is not null");
        RuleFor(r => r.Time).NotEmpty().WithMessage("Time is not null");
        RuleFor(r => r.SelectDays).NotEmpty().WithMessage("Days is not null");
    }
}