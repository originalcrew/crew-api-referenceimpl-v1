using MediatR;

namespace Crew.Api.ReferenceImpl.V1.Messages
{
    public class UpdateExampleApprovalStatusCommand : IRequest
    {
        public int Id { get; set; }
        public ApprovalStatus Status { get; set; }
    }
}