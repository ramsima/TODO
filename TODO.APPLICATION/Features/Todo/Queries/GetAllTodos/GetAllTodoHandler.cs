using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Application.Interfaces;
using TODO.APPLICATION.DTOs;

namespace TODO.APPLICATION.Features.Todo.Queries.GetAllTodos
{
    public class GetAllTodoQueryHandler(ITodoRepository _repository) : IRequestHandler<GetAllTodoQuery, IEnumerable<TodoDto>>
    {
        public async Task<IEnumerable<TodoDto>> Handle(GetAllTodoQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync(cancellationToken);
        }
    }
}
