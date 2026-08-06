using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Application.DTOs;
using Todo.Application.Interfaces;

namespace TODO.APPLICATION.Features.Todo.Commands.CreateTodo
{
    public class CreateTodoCommandHandler(ITodoRepository _repository):IRequestHandler<CreateTodoCommand,int>
    {
        public async Task<int> Handle(CreateTodoCommand request,CancellationToken cancellationoToken)
        {
            CreateTodoDto dto = new CreateTodoDto
            {
                Title = request.Title,
                Description = request.Description,
                CategoryId = request.CategogryId,
                PriorityId = request.PriorityId,
                DueDate = request.DueDate,
                TagIds = request.TagIds

            };
            return await _repository.CreateAsync(dto,cancellationoToken);
        }
    }
}
