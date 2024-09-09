namespace DAL.Entities
{
    public class Student : EntitiesBase
    {
        public Parent? Parent { get; set; }
        public Guid? ParentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StudentCode { get; set; }
        // Male: 0, Female: 1, Other: 2
        public int Gender { get; set; } = 0;
        public string? Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
    }
}
