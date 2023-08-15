using System;

namespace Crew.Api.ReferenceImpl.V1.Messages
{
    public class GetExampleResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
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