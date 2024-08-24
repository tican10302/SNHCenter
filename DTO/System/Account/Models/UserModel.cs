using DTO.Base;
using DTO.System.Account.Dtos;
using DTO.System.Role.Dtos;

namespace DTO.System.Account.Models
{
    public class UserModel : ModelBase
    {
        public AccountDto? Account { get; set; }
        public RoleDto? Role { get; set; }
        public string? StaffCode { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        // Male: 0, Female: 1, Other: 2
        public int Gender { get; set; } = 0;
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Province { get; set; }
    }
}
