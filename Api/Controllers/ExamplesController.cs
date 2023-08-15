using System;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Crew.Api.ReferenceImpl.V1.Exceptions;
using Crew.Api.ReferenceImpl.V1.Messages;
using Crew.Api.ReferenceImpl.V1.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Crew.Api.ReferenceImpl.V1.Controllers
{
    [ApiController]
    [ResponseCache(CacheProfileName = "No")]
    public class ExamplesController : ControllerBase
    {
        public ExamplesController(IMapper mapper, IMediator mediator)
        {
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public IMapper Mapper { get; }
        public IMediator Mediator { get; }

        [HttpGet]
        [Route("examples/{id:int}")]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Models.GetExampleResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            IActionResult result;

            /* create a request obj so that we can validate it & map it later */
            var request = new GetExampleRequest
            {
                Id = id
            };

            /* validate the request */
            if (!TryValidateModel(request))
            {
                result = ValidationProblem();
            }
            else
            {
                try
                {
                    /* send a query to the mediator & wait for the response */
                    Messages.GetExampleResponse response = await Mediator.Send(Mapper.Map<GetExampleQuery>(request));

                    result = Ok(Mapper.Map<Models.GetExampleResponse>(response));
                }
                catch (NotFoundException)
                {
                    result = NotFound();
                }
            }

            return result;
        }

        [HttpGet]
        [Route("examples")]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetExamplesResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Get([FromQuery] GetExamplesRequest request)
        {
            IActionResult result;

            if (request == null)
            {
                result = BadRequest();
            }
            else
            {
                /* send a query to the mediator & wait for the response */
                FindExamplesResponse response = await Mediator.Send(Mapper.Map<FindExamplesQuery>(request));

                /* decide what type of result to return */
                if (response == null)
                {
                    result = NoContent();
                }
                else
                {
                    result = Ok(Mapper.Map<GetExamplesResponse>(response));
                }
            }

            return result;
        }

        [HttpPost]
        [Route("examples")]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(PostExampleResponse), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Post(PostExampleRequest request)
        {
            IActionResult result;

            if (request == null)
            {
                result = BadRequest();
            }
            else
            {
                /* send a command to the mediator & wait for the response */
                CreateExampleResponse response = await Mediator.Send(Mapper.Map<CreateExampleCommand>(request));

                result = CreatedAtAction("Get", new { id = response.Id }, Mapper.Map<PostExampleResponse>(response));
            }

            return result;
        }

        [HttpPut]
        [Route("examples/personaldetails")]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> PutPersonalDetails(PutExamplePersonalDetailsRequest request)
        {
            IActionResult result;

            if (request == null)
            {
                result = BadRequest();
            }
            else
            {
                try
                {
                    /* send a command to the mediator & wait for the response */
                    await Mediator.Send(Mapper.Map<UpdateExamplePersonalDetailsCommand>(request));

                    result = NoContent();
                }
                catch (NotFoundException)
                {
                    result = NotFound();
                }
            }

            return result;
        }

        [HttpPut]
        [Route("examples/contactdetails")]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> PutContactDetails(PutExampleContactDetailsRequest request)
        {
            IActionResult result;

            if (request == null)
            {
                result = BadRequest();
            }
            else
            {
                try
                {
                    /* send a command to the mediator & wait for the response */
                    await Mediator.Send(Mapper.Map<UpdateExampleContactDetailsCommand>(request));

                    result = NoContent();
                }
                catch (NotFoundException)
                {
                    result = NotFound();
                }
            }

            return result;
        }

        [HttpPut]
        [Route("examples/approvalstatus")]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> PutApprovalStatus(PutExampleApprovalStatusRequest request)
        {
            IActionResult result;

            if (request == null)
            {
                result = BadRequest();
            }
            else
            {
                try
                {
                    /* send a command to the mediator & wait for the response */
                    await Mediator.Send(Mapper.Map<UpdateExampleApprovalStatusCommand>(request));

                    result = NoContent();
                }
                catch (NotFoundException)
                {
                    result = NotFound();
                }
            }

            return result;
        }

        [HttpDelete]
        [Route("examples/{id:int}")]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            IActionResult result;

            /* create a request obj so that we can validate it & map it later */
            var request = new DeleteExampleRequest
            {
                Id = id
            };

            /* validate the request */
            if (!TryValidateModel(request))
            {
                result = ValidationProblem();
            }
            else
            {
                try
                {
                    /* send a command to the mediator & wait for the response */
                    await Mediator.Send(Mapper.Map<DeleteExampleCommand>(request));

                    result = NoContent();
                }
                catch (NotFoundException)
                {
                    result = NotFound();
                }
            }

            return result;
        }
    }
}