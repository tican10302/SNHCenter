namespace DAL.Entities
{
    public class Program : EntitiesBase
    {
        public string Name { get; set; }
        public Guid? LevelId { get; set; }
        public Level? Level { get; set; }
    }
}
