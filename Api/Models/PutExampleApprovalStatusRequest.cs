namespace Crew.Api.ReferenceImpl.V1.Models
{
    public class PutExampleApprovalStatusRequest
    {
        /* # = only nullable because the caller may not have sent it. Will validate not null later. */
        public int? Id { get; set; } // #
        public ApprovalStatus? Status { get; set; } // #
    }
}