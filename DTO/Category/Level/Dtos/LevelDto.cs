using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using DTO.Base;
using FluentValidation;
using DTO.Category.Program.Dtos;


namespace DTO.Category.Level.Dtos
{
    public class LevelDto : DtoBase
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required")]
        public string? Name { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Fee is required")]
        public long Fee { get; set; }
        public string? Note { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Program is required")]
        public Guid ProgramId { get; set; }
    }
    public class LevelDtoValidator : AbstractValidator<LevelDto>
    {
        public LevelDtoValidator()
        {
            RuleFor(r => r.Name).NotEmpty().WithMessage("Name is not null");
            RuleFor(r => r.Fee).NotEmpty().WithMessage("Fee is not null");
            RuleFor(r => r.ProgramId).NotEmpty().WithMessage("Program is not null");
        }
    }
}
