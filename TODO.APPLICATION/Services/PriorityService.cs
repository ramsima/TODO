using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODO.APPLICATION.DTOs;
using Todo.Application.Interfaces;

namespace Todo.Application.Services
{
    public class PriorityService : IPriorityService
    {
        private readonly IPriorityRepository _repository;

        public PriorityService(IPriorityRepository repository)
        {
            _repository = repository;
        }

        //public async Task<IEnumerable<PriorityDto>> GetAllAsync()
        //{
        //    return await _repository.GetAllAsync();
        //}
    }
}
