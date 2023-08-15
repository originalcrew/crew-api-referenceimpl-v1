using System;
using System.Threading;
using System.Threading.Tasks;
using Crew.Api.ReferenceImpl.V1.Exceptions;
using Crew.Api.ReferenceImpl.V1.Messages;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Crew.Api.ReferenceImpl.V1.Handlers
{
    public class UpdateExamplePersonalDetailsHandler : AsyncRequestHandler<UpdateExamplePersonalDetailsCommand>
    {
        public UpdateExamplePersonalDetailsHandler(ILogger<UpdateExamplePersonalDetailsHandler> logger)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public ILogger Logger { get; }

        protected override Task Handle(UpdateExamplePersonalDetailsCommand command, CancellationToken cancellationToken)
        {
            Logger.LogInformation($"I have handled the UpdateExamplePersonalDetailsCommand for example {command.Id}");

            if (command.Id == 0)
            {
                throw new NotFoundException();
            }

            return Task.CompletedTask;
        }
    }
}