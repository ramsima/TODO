using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODO.APPLICATION.Features.Todo.Commands.DeleteTodo
{
    public record DeleteTodoCommand(
            int id
        ):IRequest;
    
}
