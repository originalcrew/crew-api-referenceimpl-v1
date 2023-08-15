using System;
using System.Threading;
using System.Threading.Tasks;
using Crew.Api.ReferenceImpl.V1.Exceptions;
using Crew.Api.ReferenceImpl.V1.Messages;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Crew.Api.ReferenceImpl.V1.Handlers
{
    public class GetExampleHandler : IRequestHandler<GetExampleQuery, GetExampleResponse>
    {
        public GetExampleHandler(ILogger<GetExampleHandler> logger)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public ILogger Logger { get; }

        public Task<GetExampleResponse> Handle(GetExampleQuery query, CancellationToken cancellationToken)
        {
            Logger.LogInformation($"I have handled the GetExampleQuery for example {query.Id}");

            if (query.Id == 0)
            {
                throw new NotFoundException();
            }

            return Task.FromResult(
                new GetExampleResponse
                {
                    Id = query.Id,
                    Name = $"Example {query.Id}",
                    CreatedDate = DateTime.Now,
                    ApprovalStatus = ApprovalStatus.Approved,
                    Mobile = $"041612345{query.Id}",
                    Address = new GetExampleResponse.AddressObj
                    {
                        UnitNumber = $"{query.Id}b",
                        StreetNumber = query.Id.ToString(),
                        StreetName = "Example Street",
                        Suburb = "Brisbane",
                        State = State.Qld,
                        Postcode = "4000"
                    }
                });
        }
    }
}