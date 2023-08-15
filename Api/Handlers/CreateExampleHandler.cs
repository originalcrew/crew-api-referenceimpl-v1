using System;
using System.Threading;
using System.Threading.Tasks;
using Crew.Api.ReferenceImpl.V1.Messages;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Crew.Api.ReferenceImpl.V1.Handlers
{
    public class CreateExampleHandler : IRequestHandler<CreateExampleCommand, CreateExampleResponse>
    {
        public CreateExampleHandler(ILogger<CreateExampleHandler> logger, IMediator mediator)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public ILogger Logger { get; }
        public IMediator Mediator { get; }

        public async Task<CreateExampleResponse> Handle(CreateExampleCommand command, CancellationToken cancellationToken)
        {
            Logger.LogInformation($"I have handled the CreateExampleCommand for example {command.Name}");

            // save

            int id = 11;

            /* publish an event to the mediator & wait for the response */
            await Mediator.Publish(
                new ExampleMobileChangedEvent
                {
                    Id = id
                });

            var response = new CreateExampleResponse
            {
                Id = id
            };

            return response;
        }
    }
}