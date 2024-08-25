using DTO.Base;
using FluentValidation;

namespace DTO.System.Menu.Dtos;

public class MenuDto : DtoBase
{
    public string? ControllerName { get; set; }
    public string? Controller { get; set; }
    public string? Action { get; set; } = "index";
    public string? Name { get; set; }
    public Guid? GroupId { get; set; }
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
        RuleFor(r => r.GroupId).NotEmpty().WithMessage("The group field is required");
    }
}