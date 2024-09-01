using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Menu
    {
        public Menu()
        {
            Sort = 0;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; } = new();
        public required string Name { get; set; }
        public required string ControllerName { get; set; }
        public required string Controller { get; set; }
        public required string Action { get; set; }
        public Guid GroupPermissionId { get; set; }
        public GroupPermission GroupPermission { get; set; }
        public string? Icon { get; set; }
        public int Sort { get; set; }
        public bool HasView { get; set; }
        public bool HasAdd { get; set; }
        public bool HasEdit { get; set; }
        public bool HasDelete { get; set; }
        public bool HasApprove { get; set; }
        public bool HasStatistic { get; set; }
        public bool IsActived { get; set; }
        public bool IsShowMenu { get; set; }
    }
}
