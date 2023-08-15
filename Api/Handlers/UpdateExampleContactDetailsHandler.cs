using System;
using System.Threading;
using System.Threading.Tasks;
using Crew.Api.ReferenceImpl.V1.Exceptions;
using Crew.Api.ReferenceImpl.V1.Messages;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Crew.Api.ReferenceImpl.V1.Handlers
{
    public class UpdateExampleContactDetailsHandler : AsyncRequestHandler<UpdateExampleContactDetailsCommand>
    {
        public UpdateExampleContactDetailsHandler(ILogger<UpdateExampleContactDetailsHandler> logger, IMediator mediator)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public ILogger Logger { get; }
        public IMediator Mediator { get; }

        protected override async Task Handle(UpdateExampleContactDetailsCommand command, CancellationToken cancellationToken)
        {
            Logger.LogInformation($"I have handled the UpdateExampleContactDetailsCommand for example {command.Id}");

            if (command.Id == 0)
            {
                throw new NotFoundException();
            }

            bool mobileHasChanged = true; // example.Mobile != command.Mobile;

            if (mobileHasChanged)
            {
                /* publish an event to the mediator & wait for the response */
                await Mediator.Publish(
                    new ExampleMobileChangedEvent
                    {
                        Id = command.Id
                    });
            }
        }
    }
}