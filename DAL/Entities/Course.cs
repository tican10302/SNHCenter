namespace DAL.Entities
{
    public class Course : EntitiesBase
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Center { get; set; }
        public string Room { get; set; }
        public Shift Shift { get; set; }
        public Guid ShiftId { get; set; }
        public Level Level { get; set; }
        public Guid LevelId { get; set; }
    }
}
