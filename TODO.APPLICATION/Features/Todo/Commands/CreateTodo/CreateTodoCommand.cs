using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODO.APPLICATION.DTOs;
using TODO.APPLICATION.Interfaces;

namespace TODO.APPLICATION.Features.Todo.Commands.CreateTodo
{
    public record CreateTodoCommand(
            string Title,
            string Description,
            int CategogryId,
            int PriorityId,
            DateTime? DueDate,
            List<int> TagIds
        ) : ICacheInvalidationCommand<int>
    {
        public IReadOnlyCollection<string> CacheKey => new List<string> { "todos:all"};
    }
    
}
