using System.Linq;
using Crew.Api.ReferenceImpl.V1.Models;
using FluentValidation;

namespace Crew.Api.ReferenceImpl.V1.Validators
{
    public class PostExampleRequestValidator : AbstractValidator<PostExampleRequest>
    {
        public PostExampleRequestValidator()
        {
            RuleFor(r => r.Name)
                .NotEmpty();

            RuleFor(r => r.Mobile)
                .NotEmpty()
                .Matches(@"^04\d{8}$")
                .WithMessage("'Mobile' must be 10 digits starting with 04.");

            RuleFor(r => r.Address)
                .NotNull();
        }
    }

    public class PostExampleRequestAddressObjValidator : AbstractValidator<PostExampleRequest.AddressObj>
    {
        public PostExampleRequestAddressObjValidator()
        {
            RuleFor(r => r.UnitNumber)
                .Must(u => !u.ToLower().Contains("unit"))
                .WithMessage("'Unit Number' must not contain the word unit.")
                .Unless(r => string.IsNullOrEmpty(r.UnitNumber));

            RuleFor(r => r.StreetNumber)
                .NotEmpty();

            RuleFor(r => r.StreetName)
                .NotEmpty();

            RuleFor(r => r.StreetName)
                .Must(
                    st =>
                    {
                        /* must contain a street type */
                        string streetType = st.Trim().Split(' ').Last();
                        string streetName = st.Replace(streetType, "").Trim();

                        return !string.IsNullOrEmpty(streetName);
                    })
                .WithMessage("'Street Name' is missing the street type eg. Avenue")
                .Unless(r => string.IsNullOrEmpty(r.StreetName));

            RuleFor(r => r.Suburb)
                .NotEmpty();

            RuleFor(r => r.State)
                .NotNull();

            RuleFor(r => r.Postcode)
                .NotEmpty()
                .Matches(@"^\d{4}$")
                .WithMessage("'Postcode' must be 4 digits.");
        }
    }
}