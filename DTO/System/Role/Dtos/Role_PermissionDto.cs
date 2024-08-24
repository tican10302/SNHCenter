using DTO.System.Role.Models;
using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace DTO.System.Role.Dtos
{
    public class Role_PermissionDto
    {
        public Guid Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "The role field is required")]
        public Guid? RoleId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "The controller name field is required")]
        public string? ControllerName { get; set; }
        public bool IsEdit { get; set; } = false;
        public bool IsApprove { get; set; } = false;
        public bool IsAdd { get; set; } = false;
        public bool IsStatistic { get; set; } = false;
        public bool IsView { get; set; } = false;
        public bool IsDelete { get; set; } = false;
        public string? Name { get; set; }
        public bool HasEdit { get; set; } = false;
        public bool HasApprove { get; set; } = false;
        public bool HasAdd { get; set; } = false;
        public bool HasStatistic { get; set; } = false;
        public bool HasView { get; set; } = false;
        public bool HasDelete { get; set; } = false;
    }

    public class Role_PermissionDtoValidator : AbstractValidator<Role_PermissionDto>
    {
        public Role_PermissionDtoValidator()
        {
            RuleFor(r => r.RoleId).NotEmpty().WithMessage("The role field is required");
            RuleFor(r => r.ControllerName).NotEmpty().WithMessage("The controller name field is required");
        }
    }
}
