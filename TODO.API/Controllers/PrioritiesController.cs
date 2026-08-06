using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Todo.Application.Services;
using TODO.APPLICATION.Features.Priorities.Queries;

namespace Todo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrioritiesController : ControllerBase
    {
        private readonly IPriorityService _service;
        private readonly IMediator _mediator;


        public PrioritiesController(IPriorityService service, IMediator mediator)
        {
            _service = service;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            //return Ok(await _service.GetAllAsync());
            return Ok(await _mediator.Send(new GetAllPrioritiesQuery(),cancellationToken));
        }
    }
}
