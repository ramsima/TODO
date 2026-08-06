using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Application.DTOs;

namespace TODO.APPLICATION.Features.Todo.Commands.CreateTodo
{
    public record CreateTodoCommand(
            string Title,
            string Description,
            int CategogryId,
            int PriorityId,
            DateTime? DueDate,
            List<int> TagIds
        ):IRequest<int>;
    
}
