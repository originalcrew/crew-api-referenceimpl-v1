using Crew.Api.ReferenceImpl.V1.Models;
using FluentValidation;

namespace Crew.Api.ReferenceImpl.V1.Validators
{
    public class DeleteExampleRequestValidator : AbstractValidator<DeleteExampleRequest>
    {
        public DeleteExampleRequestValidator()
        {
            RuleFor(r => r.Id)
                .GreaterThanOrEqualTo(0);
        }
    }
}