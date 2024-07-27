using DTO.Base;
using FluentValidation;

namespace DTO.Category.Shift.Dtos;

public class ShiftDto : BaseRequest
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Time { get; set; }
    public string? Days { get; set; }
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