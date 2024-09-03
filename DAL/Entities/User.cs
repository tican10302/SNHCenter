namespace DAL.Entities
{
    public class User : EntitiesBase
    {
        public required Account Account { get; set; }
        public required Guid AccountId { get; set; }
        public required Role Role { get; set; }
        public required Guid RoleId { get; set; }
        public required string StaffCode { get; set; }
        public required string FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime DateOfBirth { get; set; } = DateTime.Now;
        // Male: 0, Female: 1, Other: 2
        public int Gender { get; set; } = 0;
        public Provinces? Province { get; set; }
        public string? ProvinceCode { get; set; }
        public Districts? District { get; set; }
        public string? DistrictCode { get; set; }
        public Wards? Ward { get; set; }
        public string? WardCode { get; set; }
    }
}
