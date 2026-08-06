using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Application.DTOs;
using Todo.Application.Interfaces;

namespace TODO.APPLICATION.Features.Todo.Commands.UpdateTodo
{
    public class UpdateTodoCommandHandler(ITodoRepository _repository):IRequestHandler<UpdateTodoCommand>
    {
        public async Task Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
        {
            UpdateTodoDto dto = new UpdateTodoDto {
                Id = request.id,
                Title = request.title,
                Description = request.description,
                CategoryId = request.categoryid,
                PriorityId = request.priorityid,
                DueDate = request.duedate,
                IsCompleted = request.iscompleted,
                TagIds = request.tagids
            };

            await _repository.UpdateAsync(dto,cancellationToken);
        }
    }
}
