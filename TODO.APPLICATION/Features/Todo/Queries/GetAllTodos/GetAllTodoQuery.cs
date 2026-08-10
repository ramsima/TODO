using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Application.DTOs;
using TODO.APPLICATION.Interfaces;

namespace TODO.APPLICATION.Features.Todo.Queries.GetAllTodos
{
    public record GetAllTodoQuery(

        ) : ICacheableQuery<IEnumerable<TodoDto>>
    {
        public string CacheKey => "todos:all";

        public TimeSpan? Expiration => TimeSpan.FromMinutes(10);
    }
    
}
