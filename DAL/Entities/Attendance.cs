namespace DAL.Entities
{
    public class Attendance : EntitiesBase
    {
        public Student Student { get; set; }
        public Lesson Lesson { get; set; }
        public User User { get; set; }
        public bool isAbsent { get; set; }
        public string? Reason { get; set; }
    }
}
