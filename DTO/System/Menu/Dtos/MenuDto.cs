using System.ComponentModel.DataAnnotations;
using DTO.Base;
using FluentValidation;

namespace DTO.System.Menu.Dtos;

public class MenuDto : DtoBase
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "The ControllerName field is required")]
    public string? ControllerName { get; set; }
    [Required(AllowEmptyStrings = false, ErrorMessage = "The controller field is required")]
    public string? Controller { get; set; }
    [Required(AllowEmptyStrings = false, ErrorMessage = "The action field is required")]
    public string? Action { get; set; } = "index";
    [Required(AllowEmptyStrings = false, ErrorMessage = "The name field is required")]
    public string? Name { get; set; }
    public string? Icon { get; set; }
    [Required(AllowEmptyStrings = false, ErrorMessage = "The group permission field is required")]
    public Guid? GroupPermissionId { get; set; }
    public bool HasView { get; set; } = false;
    public bool HasAdd { get; set; } = false;
    public bool HasEdit { get; set; } = false;
    public bool HasDelete { get; set; } = false;
    public bool HasApprove { get; set; } = false;
    public bool HasStatistic { get; set; } = false;
    public bool IsShowMenu { get; set; } = true;
}

public class MenuDtoValidator : AbstractValidator<MenuDto>
{
    public MenuDtoValidator()
    {
        RuleFor(r => r.ControllerName).NotEmpty().WithMessage("The ControllerName field is required");
        RuleFor(r => r.Controller).NotEmpty().WithMessage("The controller field is required");
        RuleFor(r => r.Action).NotEmpty().WithMessage("The action field is required");
        RuleFor(r => r.Name).NotEmpty().WithMessage("The name field is required");
        RuleFor(r => r.GroupPermissionId).NotEmpty().WithMessage("The group permission field is required");
    }
}