using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Level : EntitiesBase
    {
        [MaxLength(500)]
        public required string Name { get; set; }
        public long Fee { get; set; }
        [MaxLength(2000)]
        public string? Note { get; set; }
        public Program? Program { get; set; }
        public Guid ProgramId { get; set; }
    }
}
