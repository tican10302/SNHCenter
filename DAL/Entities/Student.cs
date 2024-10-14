using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Student : EntitiesBase
    {
        public Parent? Parent { get; set; }
        public Guid? ParentId { get; set; }
        [MaxLength(200)]
        public required string FirstName { get; set; }
        [MaxLength(200)]
        public required string LastName { get; set; }
        [MaxLength(20)]
        public required string StudentCode { get; set; }
        // Male: 0, Female: 1, Other: 2
        public int Gender { get; set; } = 0;
        [MaxLength(12)]
        public string? Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        [MaxLength(200)]
        [EmailAddress]
        public string? Email { get; set; }
        [MaxLength(500)]
        public string? Address { get; set; }
    }
}
