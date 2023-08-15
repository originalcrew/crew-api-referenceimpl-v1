using System;
using System.Collections.Generic;

namespace Crew.Api.ReferenceImpl.V1.Models
{
    public class GetExamplesRequest
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public IList<ApprovalStatus> ApprovalStatuses { get; set; }
        public bool OrderDescending { get; set; } = true;
        public int ItemsPerPage { get; set; } = 100;
        public int? Page { get; set; }
    }
}