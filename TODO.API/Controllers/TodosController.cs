using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TODO.APPLICATION.DTOs;
using Todo.Application.Services;
using TODO.APPLICATION.Common.Exceptions;
using TODO.APPLICATION.Features.Todo.Commands.CreateTodo;
using TODO.APPLICATION.Features.Todo.Commands.DeleteTodo;
using TODO.APPLICATION.Features.Todo.Commands.UpdateTodo;
using TODO.APPLICATION.Features.Todo.Queries.GetAllTodos;
using TODO.APPLICATION.Features.Todo.Queries.GetTodoById;

namespace Todo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly ITodoService _todoService;
        private readonly IMediator _mediator;

        public TodosController(ITodoService todoService, IMediator mediator)
        {
            _todoService = todoService;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var todos = await _mediator.Send(new GetAllTodoQuery(), cancellationToken);
            //var todos = await _todoService.GetAllAsync();

            return Ok(todos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id,CancellationToken cancellationToken)
        {
            //var todo = await _todoService.GetByIdAsync(id);

            var todo = await _mediator.Send(new GetTodoByIdQuery(id),cancellationToken);

            return Ok(todo);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTodoCommand dto,CancellationToken cancellationToken)
        {
            //var id = await _todoService.CreateAsync(dto);
            var id = await _mediator.Send(dto, cancellationToken);

            return CreatedAtAction(
                nameof(GetById),
                new { id },
                id);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateTodoCommand dto,CancellationToken cancellationToken)
        {
            //await _todoService.UpdateAsync(dto);

            await _mediator.Send(dto,cancellationToken);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id,CancellationToken cancellationToken)
        {
            //await _todoService.DeleteAsync(id);

            await _mediator.Send(new DeleteTodoCommand(id),cancellationToken);

            return NoContent();
        }
    }
}
