namespace Crew.Api.ReferenceImpl.V1.Models
{
    public class PostExampleRequest
    {
        public string Name { get; set; }
        public string Mobile { get; set; }
        public AddressObj Address { get; set; }

        public class AddressObj
        {
            public string UnitNumber { get; set; }
            public string StreetNumber { get; set; }
            public string StreetName { get; set; }
            public string Suburb { get; set; }
            public State? State { get; set; } // only nullable because the caller may not have sent it. Will validate not null later.
            public string Postcode { get; set; }
        }
    }
}
