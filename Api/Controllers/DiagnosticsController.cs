using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Crew.Api.ReferenceImpl.V1.Messages;
using Crew.Api.ReferenceImpl.V1.Models;
using Crew.Api.ReferenceImpl.V1.Options;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Crew.Api.ReferenceImpl.V1.Controllers
{
    [ApiController]
    [ResponseCache(CacheProfileName = "No")]
    public class DiagnosticsController : ControllerBase
    {
        public DiagnosticsController(IOptionsMonitor<DiagnosticsOptions> diagnosticsOptionsMon, IMediator mediator)
        {
            DiagnosticsOptions = diagnosticsOptionsMon?.CurrentValue ?? throw new ArgumentNullException(nameof(diagnosticsOptionsMon));
            Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public DiagnosticsOptions DiagnosticsOptions { get; }
        public IMediator Mediator { get; }

        [HttpGet]
        [Route("diagnostics")]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(GetDiagnosticsResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get([FromHeader(Name = "Diagnostics-Key")] string key)
        {
            IActionResult result;

            if (string.IsNullOrEmpty(key) || key != DiagnosticsOptions.DiagnosticsKey)
            {
                result = Unauthorized();
            }
            else
            {
                var response = new GetDiagnosticsResponse
                {
                    MachineName = Environment.MachineName,
                    Results = await Task.WhenAll(Tests())
                };

                result = Ok(response);
            }

            return result;
        }

        private IList<Task<DiagnosticsResult>> Tests()
        {
            /*
             * The objective here is to have a test for every downstream dependency. eg databases, APIs, caches, scheduled jobs etc
             * If you depend on it, write a quick test for it!
             */
            var tests = new List<Task<DiagnosticsResult>>
            {
                Mediator.Send(
                    new TestUrlCommand
                    {
                        Key = nameof(DiagnosticsOptions.Example1ApiBaseAddress),
                        Url = $"{DiagnosticsOptions.Example1ApiBaseAddress}/version",
                        AcceptHeader = "application/json"
                    }),
                Mediator.Send(
                    new TestUrlCommand
                    {
                        Key = nameof(DiagnosticsOptions.Example2ApiBaseAddress),
                        Url = DiagnosticsOptions.Example2ApiBaseAddress,
                        AcceptHeader = "text/html"
                    })
            };

            return tests;
        }
    }
}