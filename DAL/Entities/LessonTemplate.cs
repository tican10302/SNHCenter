using System.ComponentModel.DataAnnotations;

namespace DAL.Entities;

public class LessonTemplate : EntitiesBase
{
    public int LessonNo { get; set; }
    public int? HourDone { get; set; }
    [MaxLength(500)]
    public string? CourseBookPage { get; set; }
    [MaxLength(Int32.MaxValue)]
    public string? LessonAim { get; set; }
    [MaxLength(Int32.MaxValue)]
    public string? AdditionalInformation { get; set; }
    public required CourseTemplate CourseTemplate { get; set; }
    public Guid CourseTemplateId { get; set; }
}