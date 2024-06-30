using FluentValidation;

namespace DTO.Base
{
    public class GetByIdRequest
    {
        public Guid Id { get; set; }
        
    }

    public class GetByIdDeleteRequestValidator : AbstractValidator<GetByIdRequest>
    {
        public GetByIdDeleteRequestValidator()
        {
            RuleFor(r => r.Id).NotNull().WithMessage("Id is not null");
        }
    }
}
