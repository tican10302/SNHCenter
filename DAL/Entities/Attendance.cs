namespace DAL.Entities
{
    public class Attendance : EntitiesBase
    {
        public Student Student { get; set; }
        public Guid StudentId { get; set; }
        public Lesson Lesson { get; set; }
        public Guid LessonId { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
        public bool isAbsent { get; set; }
        public string? Reason { get; set; }
    }
}
