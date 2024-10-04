using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class GroupPermission
    {
        public GroupPermission()
        {
            Sort = 0;
            IsActive = true;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; } = new();
        public required string Name { get; set; }
        public int Sort { get; set; }
        public string? Icon { get; set; }
        public bool IsActive { get; set; }
    }
}
