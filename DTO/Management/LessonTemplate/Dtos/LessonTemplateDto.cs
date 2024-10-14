using System.ComponentModel.DataAnnotations;
using DTO.Base;
using FluentValidation;

namespace DTO.Management.LessonTemplate.Dtos;

public class LessonTemplateDto : DtoBase
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Lesson no. is required")]
    public int LessonNo { get; set; }
    [Required(AllowEmptyStrings = false, ErrorMessage = "Hour done is required")]
    public int? HourDone { get; set; }
    public string? CourseBookPage { get; set; }
    public string? LessonAim { get; set; }
    public string? AdditionalInformation { get; set; }
    [Required(AllowEmptyStrings = false, ErrorMessage = "Course template is required")]
    public Guid CourseTemplateId { get; set; }
}

public class LessonTemplateDtoValidator : AbstractValidator<LessonTemplateDto>
{
    public LessonTemplateDtoValidator()
    {
        RuleFor(x => x.LessonNo).NotEmpty().WithMessage("Lesson no. is required");
        RuleFor(x => x.HourDone).NotEmpty().WithMessage("Hour done is required");
        RuleFor(x => x.CourseTemplateId).NotEmpty().WithMessage("Course template is required");
        
    }
}