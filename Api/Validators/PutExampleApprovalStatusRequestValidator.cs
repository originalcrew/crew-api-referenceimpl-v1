using Crew.Api.ReferenceImpl.V1.Models;
using FluentValidation;

namespace Crew.Api.ReferenceImpl.V1.Validators
{
    public class PutExampleApprovalStatusRequestValidator : AbstractValidator<PutExampleApprovalStatusRequest>
    {
        public PutExampleApprovalStatusRequestValidator()
        {
            RuleFor(r => r.Id)
                .NotNull()
                .GreaterThanOrEqualTo(0);

            RuleFor(r => r.Status)
                .NotNull();
        }
    }
}