namespace DAL.Entities
{
    public class User : EntitiesBase
    {
        public required Account Account { get; set; }
        public required Role Role { get; set; }
        public required string StaffCode { get; set; }
        public required string FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime DateOfBirth { get; set; } = DateTime.Now;
        // Male: 0, Female: 1, Other: 2
        public int Gender { get; set; } = 0;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public required string Province { get; set; }
    }
}
