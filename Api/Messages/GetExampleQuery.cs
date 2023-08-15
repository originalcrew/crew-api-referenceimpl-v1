using MediatR;

namespace Crew.Api.ReferenceImpl.V1.Messages
{
    public class GetExampleQuery : IRequest<GetExampleResponse>
    {
        public int Id { get; set; }
    }
}