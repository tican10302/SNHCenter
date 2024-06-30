namespace DAL.Entities
{
    public class StudentCourse : EntitiesBase
    {
        public Student Student { get; set; }
        public Course Course { get; set; }
    }
}
