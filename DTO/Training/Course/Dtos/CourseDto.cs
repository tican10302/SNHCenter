using System.ComponentModel.DataAnnotations;
using DTO.Base;
using FluentValidation;

namespace DTO.Training.Course.Dtos;

public class CourseDto : DtoBase
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required")]
    public string? Name { get; set; }
    [Required(AllowEmptyStrings = false, ErrorMessage = "Start date is required")]
    public DateTime? StartDate { get; set; }
    [Required(AllowEmptyStrings = false, ErrorMessage = "End date is required")]
    public DateTime? EndDate { get; set; }
    public string? Center { get; set; }
    [Required(AllowEmptyStrings = false, ErrorMessage = "Room is required")]
    public string? Room { get; set; }
    [Required(AllowEmptyStrings = false, ErrorMessage = "Shift is required")]
    public Guid? ShiftId { get; set; }
    [Required(AllowEmptyStrings = false, ErrorMessage = "Level is required")]
    public Guid? LevelId { get; set; }
}

public class CourseDtoValidator : AbstractValidator<CourseDto>
{
    public CourseDtoValidator()
    {
        RuleFor(r => r.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(r => r.StartDate).NotEmpty().WithMessage("Start date is required");
        RuleFor(r => r.EndDate).NotEmpty().WithMessage("End date is required");
        RuleFor(r => r.Room).NotEmpty().WithMessage("Room is required");
        RuleFor(r => r.ShiftId).NotEmpty().WithMessage("Shift is required");
        RuleFor(r => r.LevelId).NotEmpty().WithMessage("Level is required");
    }
}