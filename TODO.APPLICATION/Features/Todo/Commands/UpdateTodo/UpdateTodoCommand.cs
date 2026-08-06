using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODO.APPLICATION.Features.Todo.Commands.UpdateTodo
{
    public record UpdateTodoCommand(
            int id,
            string title,
            string? description,
            int categoryid,
            int priorityid,
            DateTime? duedate,
            bool iscompleted,
            List<int> tagids
        ):IRequest;
    
}
