using MediatR;

namespace Crew.Api.ReferenceImpl.V1.Messages
{
    public class UpdateExampleContactDetailsCommand : IRequest
    {
        public int Id { get; set; }
        public string Mobile { get; set; }
        public AddressObj Address { get; set; }

        public class AddressObj
        {
            public string UnitNumber { get; set; }
            public string StreetNumber { get; set; }
            public string StreetName { get; set; }
            public string Suburb { get; set; }
            public State State { get; set; }
            public string Postcode { get; set; }
        }
    }
}