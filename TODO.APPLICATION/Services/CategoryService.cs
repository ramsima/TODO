using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODO.APPLICATION.DTOs;
using Todo.Application.Interfaces;

namespace Todo.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        //public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        //{
        //    return await _repository.GetAllAsync();
        //}
    }
}
