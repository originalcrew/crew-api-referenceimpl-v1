using Crew.Api.ReferenceImpl.V1.Models;
using FluentValidation;

namespace Crew.Api.ReferenceImpl.V1.Validators
{
    public class PutExamplePersonalDetailsRequestValidator : AbstractValidator<PutExamplePersonalDetailsRequest>
    {
        public PutExamplePersonalDetailsRequestValidator()
        {
          RuleFor(r => r.Id)
                .NotNull()
                .GreaterThanOrEqualTo(0);

            RuleFor(r => r.Name)
                .NotEmpty();
        }
    }
}