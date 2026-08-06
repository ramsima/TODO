using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Application.DTOs;
using Todo.Application.Interfaces;

namespace TODO.APPLICATION.Features.Todo.Queries.GetTodoById
{
    public class GetTodoByIdHandler(ITodoRepository _repository):IRequestHandler<GetTodoByIdQuery,TodoDto?>
    {
        public async Task<TodoDto?> Handle(GetTodoByIdQuery request,CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(request.id, cancellationToken);
        }
    }
}
