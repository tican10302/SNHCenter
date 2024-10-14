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
        public string? Province { get; set; }
        public Guid ProvinceId { get; set; }
        public string? District { get; set; }
        public Guid DistrictId { get; set; }
        public string? Ward { get; set; }
        public Guid WardId { get; set; }

    }
}
