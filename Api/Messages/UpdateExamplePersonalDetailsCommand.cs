using MediatR;

namespace Crew.Api.ReferenceImpl.V1.Messages
{
    public class UpdateExamplePersonalDetailsCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}