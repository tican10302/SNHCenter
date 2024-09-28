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
        public long Fee { get; set; }
    }
    public class ProgramDtoValidator : AbstractValidator<LevelDto>
    {
        public ProgramDtoValidator()
        {
            RuleFor(r => r.Name).NotEmpty().WithMessage("Name is not null");
        }
    }
}
