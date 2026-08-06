using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Todo.Application.Services;
using TODO.APPLICATION.Features.Categories.Queries.GetAllCategories;

namespace Todo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _service;
        private readonly IMediator _mediator;

        public CategoriesController(ICategoryService service,IMediator mediator)
        {
            _service = service;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            //return Ok(await _service.GetAllAsync());
            return Ok(await _mediator.Send(new GetAllCategoriesQuery(),cancellationToken));
        }
    }
}
