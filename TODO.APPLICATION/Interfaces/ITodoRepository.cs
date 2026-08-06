using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Application.DTOs;

namespace Todo.Application.Interfaces
{
    public interface ITodoRepository
    {
        Task<IEnumerable<TodoDto>> GetAllAsync(CancellationToken cancellationToken);

        Task<TodoDto?> GetByIdAsync(int id, CancellationToken cancellationToken);

        Task<int> CreateAsync(CreateTodoDto dto,CancellationToken cancellationToken);

        Task UpdateAsync(UpdateTodoDto dto,CancellationToken cancellationToken);

        Task DeleteAsync(int id,CancellationToken cancellationToken);
    }
}
