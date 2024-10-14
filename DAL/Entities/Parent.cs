using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Parent : EntitiesBase
    {
        [MaxLength(500)]
        public required string FirstName { get; set; }
        [MaxLength(500)]
        public required string LastName { get; set; }
        [MaxLength(12)]
        public required string Phone { get; set; }
        [MaxLength(500)]
        [EmailAddress]
        public string? Email { get; set; }
        public Provinces? Province { get; set; }
        public Guid? ProvinceId { get; set; }
        public Districts? District { get; set; }
        public Guid? DistrictId { get; set; }
        public Wards? Ward { get; set; }
        public Guid? WardId { get; set; }
    }
}
