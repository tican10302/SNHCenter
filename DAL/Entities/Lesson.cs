namespace DAL.Entities
{
    public class Lesson : EntitiesBase
    {
        public int LessonNo { get; set; }
        public int HourDone { get; set; }
        public DateTime Date { get; set; }
        public string CourseBookPage { get; set; }
        public string LessonAim { get; set; }
        public string AdditionalInformation { get; set; }
        public Course Course { get; set; }
        public User User { get; set; }
        public Guid HomeworkId { get; set; }
        public Homework Homework { get; set; }
    }
}
