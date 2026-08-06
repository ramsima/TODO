using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Application.DTOs;
using Todo.Application.Interfaces;

namespace Todo.Application.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _repository;

        public TagService(ITagRepository repository)
        {
            _repository = repository;
        }

        //public async Task<IEnumerable<TagDto>> GetAllAsync()
        //{
        //    return await _repository.GetAllAsync();
        //}
    }
}
