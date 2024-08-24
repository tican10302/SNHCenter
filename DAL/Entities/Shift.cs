using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Shift : EntitiesBase
    {
        public required string Name { get; set; }
        public required TimeSpan Time { get; set; }
        public required string Days { get; set; }
    }
}
