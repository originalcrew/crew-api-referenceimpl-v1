using MediatR;

namespace Crew.Api.ReferenceImpl.V1.Messages
{
    public class DeleteExampleCommand : IRequest
    {
        public int Id { get; set; }
    }
}