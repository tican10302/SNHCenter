using System.ComponentModel.DataAnnotations;
using DTO.Base;
using FluentValidation;

namespace DTO.Management.Parent.Dtos;

public class ParentDto : DtoBase
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "First Name is required")]
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public Guid ProvinceId { get; set; }
    public Guid DistrictId { get; set; }
    public Guid WardId { get; set; }
    public string? Note { get; set; }
}
public class ParentDtoValidator : AbstractValidator<ParentDto>
{
    public ParentDtoValidator()
    {
        RuleFor(r => r.FirstName).NotEmpty().WithMessage("First Name is not null");
        RuleFor(r => r.LastName).NotEmpty().WithMessage("Last Name is not null");
        RuleFor(r => r.ProvinceId).NotEmpty().WithMessage("Province is not null");
        RuleFor(r => r.DistrictId).NotEmpty().WithMessage("District is not null");
        RuleFor(r => r.WardId).NotEmpty().WithMessage("Ward is not null");
    }
}