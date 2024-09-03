using System.ComponentModel.DataAnnotations;
using DTO.Base;
using DTO.Category.Province.Dtos;
using DTO.System.Role.Dtos;
using FluentValidation;

namespace DTO.System.Account.Dtos
{
    public class UserDto : DtoBase
    {
        public AccountDto? Account { get; set; } = new AccountDto();
        public RoleDto? Role { get; set; }  = new RoleDto();
        [Required(AllowEmptyStrings = false, ErrorMessage = "Staff code is required")]
        public string? StaffCode { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "First name is required")]
        public string? FirstName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Last name is required")]
        public string? LastName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Date of birth is required")]
        public DateTime? DateOfBirth { get; set; }
        // Male: 0, Female: 1, Other: 2
        [Required(AllowEmptyStrings = false, ErrorMessage = "Gender is required")]
        public int Gender { get; set; } = 0;
        public string? Address { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Province is required")]
        public string? ProvinceCode { get; set; }
        public string? DistrictCode { get; set; }
        public string? WardCode { get; set; }
    }

    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator()
        {
            RuleFor(r => r.StaffCode).NotEmpty().WithMessage("Staff code is required");
            RuleFor(r => r.FirstName).NotEmpty().WithMessage("First name is required");
            RuleFor(r => r.LastName).NotEmpty().WithMessage("Last name is required");
            RuleFor(r => r.DateOfBirth).NotEmpty().WithMessage("Date of birth is required");
            RuleFor(r => r.Gender).NotEmpty().WithMessage("Gender is required");
            RuleFor(r => r.ProvinceCode).NotEmpty().WithMessage("Province is required");
        }
    }
}
