using System.Collections.Generic;

namespace Crew.Api.ReferenceImpl.V1.Models
{
    public class GetDiagnosticsResponse
    {
        public string MachineName { get; set; }
        public IList<DiagnosticsResult> Results { get; set; }
    }
}