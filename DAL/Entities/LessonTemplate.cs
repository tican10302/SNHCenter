namespace DAL.Entities;

public class LessonTemplate : EntitiesBase
{
    public int LessonNo { get; set; }
    public int? HourDone { get; set; }
    public string? CourseBookPage { get; set; }
    public string? LessonAim { get; set; }
    public string? AdditionalInformation { get; set; }
    public required CourseTemplate CourseTemplate { get; set; }
    public Guid CourseTemplateId { get; set; }
}