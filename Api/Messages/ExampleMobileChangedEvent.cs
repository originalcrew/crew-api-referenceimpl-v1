using MediatR;

namespace Crew.Api.ReferenceImpl.V1.Messages
{
    public class ExampleMobileChangedEvent : INotification
    {
        public int Id { get; set; }
    }
}