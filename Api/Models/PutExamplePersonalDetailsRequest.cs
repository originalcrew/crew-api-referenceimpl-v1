namespace Crew.Api.ReferenceImpl.V1.Models
{
    public class PutExamplePersonalDetailsRequest
    {
        public int? Id { get; set; } // only nullable because the caller may not have sent it. Will validate not null later.
        public string Name { get; set; }
    }
}
