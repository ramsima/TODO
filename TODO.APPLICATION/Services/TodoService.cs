using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Application.DTOs;
using Todo.Application.Interfaces;

namespace Todo.Application.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _repository;

        public TodoService(ITodoRepository repository)
        {
            _repository = repository;
        }
        //public async Task<int> CreateAsync(CreateTodoDto dto)
        //{
        //    if (string.IsNullOrWhiteSpace(dto.Title))
        //        throw new ArgumentException("Title is required");

        //    return await _repository.CreateAsync(dto);
        //}

        //public async Task DeleteAsync(int id)
        //{
        //    await _repository.DeleteAsync(id);
        //}

        //public async Task<IEnumerable<TodoDto>> GetAllAsync()
        //{
        //    return await _repository.GetAllAsync();
        //}

        //public async Task<TodoDto?> GetByIdAsync(int id)
        //{
        //    return await _repository.GetByIdAsync(id);
        //}

        //public async Task UpdateAsync(UpdateTodoDto dto)
        //{
        //    if (string.IsNullOrWhiteSpace(dto.Title))
        //        throw new ArgumentException("Title is required");

        //    await _repository.UpdateAsync(dto);
        //}
    }
}
