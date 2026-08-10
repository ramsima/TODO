using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TODO.APPLICATION.Interfaces;

namespace TODO.APPLICATION.Features.Todo.Commands.DeleteTodo
{
    public record DeleteTodoCommand(
            int id
        ):ICacheInvalidationCommand<bool>
    {
        public IReadOnlyCollection<string> CacheKey => new List<string> { "todos:all",$"todos:{id}" };
    }
    
}
