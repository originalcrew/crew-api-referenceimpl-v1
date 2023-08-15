using MediatR;

namespace Crew.Api.ReferenceImpl.V1.Messages
{
    public class TestUrlCommand : IRequest<DiagnosticsResult>
    {
        public string Key { get; set; }
        public string Url { get; set; }
        public string AcceptHeader { get; set; }
    }
}