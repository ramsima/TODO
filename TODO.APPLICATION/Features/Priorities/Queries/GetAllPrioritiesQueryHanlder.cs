using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Application.Interfaces;
using TODO.APPLICATION.DTOs;

namespace TODO.APPLICATION.Features.Priorities.Queries
{
    public class GetAllPrioritiesQueryHanlder(IPriorityRepository _repository):IRequestHandler<GetAllPrioritiesQuery,IEnumerable<PriorityDto>>
    {
        public async Task<IEnumerable<PriorityDto>> Handle(GetAllPrioritiesQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync(cancellationToken);
        }
    }
}
