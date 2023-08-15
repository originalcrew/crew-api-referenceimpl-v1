using System;
using System.Threading;
using System.Threading.Tasks;
using Crew.Api.ReferenceImpl.V1.Messages;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Crew.Api.ReferenceImpl.V1.Handlers
{
    public class ExampleMobileChangedHandler : INotificationHandler<ExampleMobileChangedEvent>
    {
        public ExampleMobileChangedHandler(ILogger<ExampleMobileChangedHandler> logger)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public ILogger Logger { get; }

        public Task Handle(ExampleMobileChangedEvent @event, CancellationToken cancellationToken)
        {
            Logger.LogInformation($"I have handled the ExampleMobileChangedEvent for example {@event.Id}");

            // do something - eg. create a validation code & send it via SMS

            return Task.CompletedTask;
        }
    }
}