using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using Todo.Application.Services;
using TODO.APPLICATION.Features.Priorities.Queries;
using TODO.APPLICATION.Features.Tags.Queries;

namespace Todo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ITagService _service;
        private readonly IMediator _mediator;

        public TagsController(ITagService service, IMediator mediator)
        {
            _service = service;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            //return Ok(await _service.GetAllAsync());
            return Ok(await _mediator.Send(new GetAllTagsQuery(), cancellationToken));
        }
    }
}
