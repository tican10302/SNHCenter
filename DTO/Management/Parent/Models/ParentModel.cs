using DTO.Base;

namespace DTO.Management.Parent.Models
{
    public class ParentModel : ModelBase
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Note { get; set; }
    }
}
