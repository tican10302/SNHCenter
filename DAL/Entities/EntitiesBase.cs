using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class EntitiesBase
    {
        public EntitiesBase()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;;
            IsDeleted = false;
            IsActived = true;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; } = new();
        public DateTime CreatedAt { get; set; }
        public required string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string? DeletedBy { get; set; }
        public bool IsActived { get; set; }
        public bool IsDeleted { get; set; }
    }
}
