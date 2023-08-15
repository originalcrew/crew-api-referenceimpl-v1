using System.Linq;
using Crew.Api.ReferenceImpl.V1.Models;
using FluentValidation;

namespace Crew.Api.ReferenceImpl.V1.Validators
{
    public class GetExamplesRequestValidator : AbstractValidator<GetExamplesRequest>
    {
        public GetExamplesRequestValidator()
        {
            RuleFor(r => r.FromDate)
                .LessThanOrEqualTo(r => r.ToDate)
                .Unless(r => r.ToDate == null);

            RuleFor(r => r.ToDate)
                .GreaterThanOrEqualTo(r => r.FromDate)
                .Unless(r => r.FromDate == null);

            RuleFor(r => r.ApprovalStatuses)
                .Must(ass => ass.Distinct().Count() == ass.Count)
                .WithMessage("ApprovalStatuses must be distinct.")
                .Unless(r => r.ApprovalStatuses == null);

            RuleFor(r => r.ItemsPerPage)
                .GreaterThan(0);

            RuleFor(r => r.Page)
                .GreaterThan(0)
                .Unless(r => r.Page == null);
        }
    }
}