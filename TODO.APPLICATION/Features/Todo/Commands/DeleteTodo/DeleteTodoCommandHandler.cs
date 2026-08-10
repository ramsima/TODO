using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Application.Interfaces;

namespace TODO.APPLICATION.Features.Todo.Commands.DeleteTodo
{
    public class DeleteTodoCommandHandler(ITodoRepository _repository):IRequestHandler<DeleteTodoCommand,bool>
    {
        public async Task<bool> Handle(DeleteTodoCommand request,CancellationToken cancellationToken)
        {
            return await _repository.DeleteAsync(request.id,cancellationToken);
        }
    }
}
