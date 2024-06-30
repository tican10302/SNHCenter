namespace DAL.Entities
{
    public class Homework : EntitiesBase
    {
        public Student Student { get; set; }
        public Lesson Lesson { get; set; }
        public int Score { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
    }
}
