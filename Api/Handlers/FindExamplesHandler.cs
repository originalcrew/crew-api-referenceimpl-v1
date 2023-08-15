using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Crew.Api.ReferenceImpl.V1.Messages;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Crew.Api.ReferenceImpl.V1.Handlers
{
    public class FindExamplesHandler : IRequestHandler<FindExamplesQuery, FindExamplesResponse>
    {
        public FindExamplesHandler(ILogger<FindExamplesHandler> logger)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public ILogger Logger { get; }

        public Task<FindExamplesResponse> Handle(FindExamplesQuery query, CancellationToken cancellationToken)
        {
            Logger.LogInformation("I have handled the FindExamplesQuery");

            var examples = new List<FindExamplesResponse.Example>();

            for (int i = 1; i < 11; i++)
            {
                examples.Add(
                    new FindExamplesResponse.Example
                    {
                        Id = i,
                        Name = $"Example {i}",
                        CreatedDate = DateTime.Now,
                        ApprovalStatus = ApprovalStatus.Pending
                    });
            }

            // apply filtering & page count here

            return Task.FromResult(
                new FindExamplesResponse
                {
                    Examples = examples,
                    TotalPagesAvailable = 1
                });
        }
    }
}