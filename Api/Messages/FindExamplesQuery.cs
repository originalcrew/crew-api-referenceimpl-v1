using System;
using System.Collections.Generic;
using MediatR;

namespace Crew.Api.ReferenceImpl.V1.Messages
{
    public class FindExamplesQuery : IRequest<FindExamplesResponse>
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public IList<ApprovalStatus> ApprovalStatuses { get; set; }
        public bool OrderDescending { get; set; }
        public int ItemsPerPage { get; set; }
        public int? Page { get; set; }
    }
}