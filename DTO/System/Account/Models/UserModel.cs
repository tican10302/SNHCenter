using DTO.Base;
using DTO.System.Account.Dtos;
using DTO.System.Role.Dtos;
using DTO.System.Role.Models;

namespace DTO.System.Account.Models
{
    public class UserModel : ModelBase
    {
        public AccountModel? Account { get; set; }
        public RoleModel? Role { get; set; }
        public string? StaffCode { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        // Male: 0, Female: 1, Other: 2
        public int Gender { get; set; } = 0;
        public string? Province { get; set; }
        public string? ProvinceCode { get; set; }
        public string? District { get; set; }
        public string? DistrictCode { get; set; }
        public string? Ward { get; set; }
        public string? WardCode { get; set; }
    }
}
