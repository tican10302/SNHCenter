using System.ComponentModel.DataAnnotations;
using DTO.Base;
using FluentValidation;

namespace DTO.Management.CourseTemplate.Dtos;

public class CourseTemplateDto : DtoBase
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required")]
    public string? Name { get; set; }
    [Required(AllowEmptyStrings = false, ErrorMessage = "Level is required")]
    public Guid? LevelId { get; set; }
}

public class CourseTemplateDtoValidator : AbstractValidator<CourseTemplateDto>
{
    public CourseTemplateDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.LevelId).NotEmpty().WithMessage("Level is required");
    }
}