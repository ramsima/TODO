using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODO.APPLICATION.DTOs;
using TODO.APPLICATION.Interfaces;

namespace TODO.APPLICATION.Features.Todo.Queries.GetTodoById
{
    public record GetTodoByIdQuery(
            int id
        ) : ICacheableQuery<TodoDto?>
    {
        public string CacheKey => $"todos:{id}";

        public TimeSpan? Expiration => TimeSpan.FromMinutes(10);
    }
    
}
