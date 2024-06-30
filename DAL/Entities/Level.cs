namespace DAL.Entities
{
    public class Level : EntitiesBase
    {
        public string Name { get; set; }
        public long Fee { get; set; }
        public Program Program { get; set; }
        public Guid CourseId { get; set; }
        public Course Course { get; set; }
    }
}
