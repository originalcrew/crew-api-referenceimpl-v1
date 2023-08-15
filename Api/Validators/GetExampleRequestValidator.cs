using Crew.Api.ReferenceImpl.V1.Models;
using FluentValidation;

namespace Crew.Api.ReferenceImpl.V1.Validators
{
    public class GetExampleRequestValidator : AbstractValidator<GetExampleRequest>
    {
        public GetExampleRequestValidator()
        {
            RuleFor(r => r.Id)
                .GreaterThanOrEqualTo(0);
        }
    }
}