using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODO.APPLICATION.DTOs;

namespace Todo.Application.Interfaces
{
    public interface IPriorityRepository
    {
        Task<IEnumerable<PriorityDto>> GetAllAsync(CancellationToken cancellationToken);
    }
}
