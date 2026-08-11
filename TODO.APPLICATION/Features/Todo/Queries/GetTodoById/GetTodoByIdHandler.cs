using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Application.Interfaces;
using TODO.APPLICATION.Common.Exceptions;
using TODO.APPLICATION.DTOs;

namespace TODO.APPLICATION.Features.Todo.Queries.GetTodoById
{
    public class GetTodoByIdHandler(ITodoRepository _repository):IRequestHandler<GetTodoByIdQuery,TodoDto?>
    {
        public async Task<TodoDto?> Handle(GetTodoByIdQuery request,CancellationToken cancellationToken)
        {
            var result = await _repository.GetByIdAsync(request.id, cancellationToken);
            if(result == null)
            {
                throw new NotFoundException($"Todo with Id {request.id} doesnot exist.");
            }
            return result;
        }
    }
}
