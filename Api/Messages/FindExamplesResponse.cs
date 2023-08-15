using System;
using System.Collections.Generic;

namespace Crew.Api.ReferenceImpl.V1.Messages
{
    public class FindExamplesResponse
    {
        public IList<Example> Examples { get; set; }
        public int TotalPagesAvailable { get; set; }

        public class Example
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public DateTime CreatedDate { get; set; }
            public ApprovalStatus ApprovalStatus { get; set; }
        }
    }
}