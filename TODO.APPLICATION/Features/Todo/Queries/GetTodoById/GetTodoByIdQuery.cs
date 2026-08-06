using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Application.DTOs;

namespace TODO.APPLICATION.Features.Todo.Queries.GetTodoById
{
    public record GetTodoByIdQuery(
            int id
        ):IRequest<TodoDto?>;
    
}
