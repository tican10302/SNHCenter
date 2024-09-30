using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Lesson : EntitiesBase
    {
        public int LessonNo { get; set; }
        public int HourDone { get; set; }
        public DateTime Date { get; set; }
        [MaxLength(2000)]
        public string? CourseBookPage { get; set; }
        [MaxLength(2000)]
        public string? LessonAim { get; set; }
        [MaxLength(2000)]
        public string? AdditionalInformation { get; set; }
        public User? User { get; set; }
        public Guid? UserId { get; set; }
        public Course? Course { get; set; }
        public Guid CourseId { get; set; }
    }
}
