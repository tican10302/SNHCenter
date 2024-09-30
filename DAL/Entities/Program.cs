using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Program : EntitiesBase
    {
        [MaxLength(500)]
        public required string Name { get; set; }
        [MaxLength(2000)]
        public string? Note { get; set; }
    } 
}
