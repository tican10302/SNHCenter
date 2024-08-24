using DTO.Base;
using FluentValidation;

namespace DTO.System.GroupPermission.Dtos;

public class GroupPermissionDto : DtoBase
{
    public string? Name { get; set; }
    public string? Icon { get; set; }
}

public class GroupPermissionDtoValidator : AbstractValidator<GroupPermissionDto>
{
    public GroupPermissionDtoValidator()
    {
        RuleFor(r => r.Name).NotEmpty().WithMessage("The name field is not null");
        RuleFor(r => r.Sort).NotNull().WithMessage("The sort field is not null");
    }
}