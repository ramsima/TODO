using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Application.Interfaces;

namespace TODO.APPLICATION.Features.Todo.Commands.DeleteTodo
{
    public class DeleteTodoCommandHandler(ITodoRepository _repository):IRequestHandler<DeleteTodoCommand>
    {
        public async Task Handle(DeleteTodoCommand request,CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(request.id,cancellationToken);
        }
    }
}
