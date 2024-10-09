using System.ComponentModel.DataAnnotations;
using DTO.Base;
using FluentValidation;

namespace DTO.Management.Parent.Dtos;

public class ParentDto : DtoBase
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "First Name is required")]
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? Note { get; set; }
}
public class ParentDtoValidator : AbstractValidator<ParentDto>
{
    public ParentDtoValidator()
    {
        RuleFor(r => r.FirstName).NotEmpty().WithMessage("First Name is not null");
        RuleFor(r => r.LastName).NotEmpty().WithMessage("Last Name is not null");
    }
}